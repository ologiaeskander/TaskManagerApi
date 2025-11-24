using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Services.Interfaces;
using RegisterModel = TaskManagerApi.Models.RegisterModel;
//using LoginModel = TaskManagerApi.Models.LoginModel;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }
        //[HttpPost("token")]
        //public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        //{
        //    var result = await _userService.GetTokenAsync(model);
        //    return Ok(result);
        //}
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(TokenRequestModel model)
        {
            var result = await _userService.LoginAsync(model);
            return Ok(result);
        }
        [HttpGet("manage/{userId}")]
        public async Task<IActionResult> ManageAsync(string userId)
        {
            var result = await _userService.ManageAsync(userId);
            return Ok(result);
        }

        [HttpPut("manage/{userId}/{role}")]
        public async Task<IActionResult> ManageRoleAsync(string userId, string role)
        {
            await _userService.ManageRoleAsync(userId, role);
            return Ok(new { Message = "Role updated successfully." });
        }
    }
}
