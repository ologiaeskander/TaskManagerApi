using TaskManagerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        //todo SQL commands go here, I think?
        [HttpGet]
        public ActionResult<List<Job>> GetTasks() //todo the list thing is temporary
        {
            //return jobs
            return Ok(0); //todo also temporary
        }
        [HttpGet("{id}")]
        public ActionResult<List<Job>> GetTaskById(int id) //todo the list thing is temporary
        {
            //Some SQL return where command to get specific jobs, etc...
            return Ok(1); //todo also temporary
        }
        //[HttpPost] for adding
        //[HttpPut] for updating
        //Deleting

    }
}
