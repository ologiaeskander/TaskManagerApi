using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Task Manager API is running");
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
    }
}