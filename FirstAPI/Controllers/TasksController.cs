using FirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task = FirstAPI.Models.Task;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        //todo SQL commands go here, I think?
        [HttpGet]
        public ActionResult<List<Task>> GetTasks() //todo the list thing is temporary
        {
            //return tasks
            return Ok(0); //todo also temporary
        }
        [HttpGet("{id}")]
        public ActionResult<List<Task>> GetTaskById(int id) //todo the list thing is temporary
        {
            //Some SQL return where command to get specific tasks, etc...
            return Ok(1); //todo also temporary
        }
        //[HttpPost]
    }
}
