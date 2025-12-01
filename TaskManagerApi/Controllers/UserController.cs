using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Services.Interfaces;
using LoginModel = TaskManagerApi.Models.LoginModel;
using RegisterModel = TaskManagerApi.Models.RegisterModel;
//using LoginModel = TaskManagerApi.Models.LoginModel;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, ITokenService tokenService, IRoleService roleService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _roleService = roleService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }
        [HttpPost("token")]
        public async Task<IActionResult> GenerateJwtToken(ApplicationUser model)
        {
            var result = await _tokenService.GenerateJwtToken(model);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginModel model)
        {
            var result = await _userService.LoginAsync(model);
            return Ok(result);
        }
    }
}
