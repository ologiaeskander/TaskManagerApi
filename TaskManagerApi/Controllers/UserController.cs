using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
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
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
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
    }
}
