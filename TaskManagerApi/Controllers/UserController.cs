using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using RegisterModel = TaskManagerApi.Models.RegisterModel;
//using RegisterModel = TaskManagerApi.Areas.Identity.Pages.Account.RegisterModel;

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
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }
        //[HttpPost("login")]
        //public async Task<ActionResult> LoginASync(LoginModel model)
        //{
        //    var result = await _userService.LoginASync(model);
        //    return Ok(result);
        //}
    }
}
