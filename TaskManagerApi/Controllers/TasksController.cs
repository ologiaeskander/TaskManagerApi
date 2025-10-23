using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public JobsController(TaskManagerContext context)
        {
            _context = context;
        }

        //// GET all jobs
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        //{
        //    var jobs = await _context.Jobs.ToListAsync();
        //    return Ok(jobs);
        //}

        // GET job by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
                return NotFound($"Job with ID {id} not found.");

            return Ok(job);
        }

        //GET job by Parameters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs(
            [FromQuery] int? assignedToUserId,
            [FromQuery] Status? status,
            [FromQuery] Priority? priority,
            [FromQuery] bool? excludeDone,
            [FromQuery] DateTime? dueBefore,
            [FromQuery] DateTime? dueAfter)
        {
            var query = _context.Jobs.AsQueryable();

            if (assignedToUserId.HasValue)
                query = query.Where(j => j.AssignedToUserId == assignedToUserId.Value);

            if (status.HasValue)
                query = query.Where(j => j.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(j => j.Priority == priority.Value);

            if (excludeDone.HasValue && excludeDone.Value)
                query = query.Where(j => j.Status != Status.Done);

            if (dueBefore.HasValue)
                query = query.Where(j => j.DueDate.HasValue && j.DueDate.Value <= dueBefore.Value);

            if (dueAfter.HasValue)
                query = query.Where(j => j.DueDate.HasValue && j.DueDate.Value >= dueAfter.Value);

            var jobs = await query.ToListAsync();
            return Ok(jobs);
        }

        // GET job(s) by title
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title parameter is required.");

            var jobs = await _context.Jobs
                .Where(j => j.Title.Contains(title))
                .ToListAsync();

            if (!jobs.Any())
                return NotFound($"No jobs found containing '{title}'.");

            return Ok(jobs);
        }



        // POST (create a new job)
        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob(Job job)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //todo might specify error responses later

            job.CreatedAt = DateTime.UtcNow; // auto-set timestamp

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
        }

        // PUT (update an existing job)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, Job job)
        {
            if (id != job.Id)
                return BadRequest("Job ID mismatch.");

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Jobs.Any(e => e.Id == id))
                    return NotFound($"Job with ID {id} not found.");

                throw; //better safe than sorry
            }

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
                return NotFound($"Job with ID {id} not found.");

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}