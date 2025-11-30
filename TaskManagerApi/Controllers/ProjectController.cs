using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;
using MediatR;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET project by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectModel>> GetProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            
            return Ok(project);
        }

        // GET all projects with optional filtering
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects(
            [FromQuery] string? creatorId)
        {
            var projects = await _projectRepository.GetAllAsync(
                filter: p => string.IsNullOrEmpty(creatorId) || p.CreatorId == creatorId,
                orderBy: q => q.OrderBy(p => p.Name),
                includeProperties: "Creator"
            );

            return Ok(projects);
        }

        // GET projects by name search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name parameter is required.");

            var projects = await _projectRepository.GetAllAsync(
                filter: p => p.Name.Contains(name),
                includeProperties: "Creator"
            );

            return Ok(projects);
        }

        // GET projects by specific user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsByUser(string userId)
        {
            var projects = await _projectRepository.GetProjectsByUserAsync(userId);
            return Ok(projects);
        }

        // POST (create a new project)
        [HttpPost]
        public async Task<ActionResult<ProjectModel>> CreateProject(ProjectModel project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            project.CreatedAt = DateTime.UtcNow;

            await _projectRepository.AddAsync(project);
            var saved = await _projectRepository.SaveChangesAsync();

            if (!saved)
                return BadRequest("Failed to create project.");

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT (update an existing project)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectModel project)
        {
            if (id != project.Id)
                return BadRequest("Project ID mismatch.");

            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                return NotFound($"Project with ID {id} not found.");

            _projectRepository.Update(project);
            var saved = await _projectRepository.SaveChangesAsync();

            if (!saved)
                return BadRequest("Failed to update project.");

            return NoContent();
        }

        // DELETE (if you want to add this functionality)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            
            await _projectRepository.DeleteAsync(id);
            var saved = await _projectRepository.SaveChangesAsync();

            if (!saved)
                return BadRequest("Failed to delete project.");

            return NoContent();
        }
    }
}