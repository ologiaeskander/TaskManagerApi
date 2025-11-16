using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;

        public JobsController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        // GET job by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return Ok($"Job with ID {id} not found.");

            return Ok(job);
        }

        // GET jobs with flexible filtering
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs(
            [FromQuery] string? assignedToUserId,
            [FromQuery] Status? status,
            [FromQuery] Priority? priority,
            [FromQuery] bool? excludeDone,
            [FromQuery] DateTime? dueBefore,
            [FromQuery] DateTime? dueAfter)
        {
            // Build the filter expression dynamically
            var jobs = await _jobRepository.GetAllAsync(
                filter: j =>
                    (string.IsNullOrEmpty(assignedToUserId) || j.AssignedToUserId == assignedToUserId) &&
                    (!status.HasValue || j.Status == status.Value) &&
                    (!priority.HasValue || j.Priority == priority.Value) &&
                    (!excludeDone.HasValue || !excludeDone.Value || j.Status != Status.Done) &&
                    (!dueBefore.HasValue || (j.DueDate.HasValue && j.DueDate.Value <= dueBefore.Value)) &&
                    (!dueAfter.HasValue || (j.DueDate.HasValue && j.DueDate.Value >= dueAfter.Value)),
                orderBy: q => q.OrderBy(j => j.DueDate ?? DateTime.MaxValue), // Handle null due dates
                includeProperties: "AssignedToUser,Project,Creator"
            );

            return Ok(jobs);
        }

        // GET jobs by title search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title parameter is required.");

            var jobs = await _jobRepository.GetAllAsync(
                filter: j => j.Title.Contains(title),
                includeProperties: "AssignedToUser,Project,Creator"
            );

            return Ok(jobs);
        }

        // GET jobs by specific user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsByUser(string userId)
        {
            var jobs = await _jobRepository.GetJobsByUserAsync(userId);
            return Ok(jobs);
        }

        // GET jobs by status
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsByStatus(Status status)
        {
            var jobs = await _jobRepository.GetJobsByStatusAsync(status);
            return Ok(jobs);
        }

        // GET overdue jobs
        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<Job>>> GetOverdueJobs()
        {
            var jobs = await _jobRepository.GetOverdueJobsAsync();
            return Ok(jobs);
        }

        // POST (create a new job)
        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob(Job job)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            job.CreatedAt = DateTime.UtcNow;

            await _jobRepository.AddAsync(job);
            var saved = await _jobRepository.SaveChangesAsync();

            if (!saved)
                return BadRequest("Failed to create job.");

            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
        }

        // PUT (update an existing job)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, Job job)
        {
            if (id != job.Id)
                return BadRequest("Job ID mismatch.");

            var existingJob = await _jobRepository.GetByIdAsync(id);
            if (existingJob == null)
                return NotFound($"Job with ID {id} not found.");

            _jobRepository.Update(job);
            var saved = await _jobRepository.SaveChangesAsync();

            if (!saved)
                return BadRequest("Failed to update job.");

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return NotFound($"Job with ID {id} not found.");

            await _jobRepository.DeleteAsync(id);
            var saved = await _jobRepository.SaveChangesAsync();

            if (!saved)
                return BadRequest("Failed to delete job.");

            return NoContent();
        }
    }
}