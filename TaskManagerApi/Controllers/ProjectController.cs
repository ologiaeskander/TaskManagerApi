using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public ProjectsController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET project by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            return Ok(project);
        }

        // GET all projects (with optional filtering by creator)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects(
            [FromQuery] int? createdBy)
        {
            var query = _context.Projects.AsQueryable();

            if (createdBy.HasValue)
                query = query.Where(j => j.CreatedBy == createdBy.Value);

            var projects = await query.ToListAsync();
            return Ok(projects);
        }

        // GET project(s) by name
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name parameter is required.");

            var projects = await _context.Projects
                .Where(p => p.Name.Contains(name))
                .ToListAsync();

            if (!projects.Any())
                return NotFound($"No projects found containing '{name}'.");

            return Ok(projects);
        }

        // POST (create a new project)
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            project.CreatedAt = DateTime.UtcNow; // auto-set timestamp

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT (update an existing project)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.Id)
                return BadRequest("Project ID mismatch.");

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(e => e.Id == id))
                    return NotFound($"Project with ID {id} not found.");
                throw;
            }

            return NoContent();
        }

    }
}
