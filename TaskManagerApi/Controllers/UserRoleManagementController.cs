using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services.Interfaces;

namespace TaskManagerApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserRoleManagementController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserRoleManagementController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet("manage/{userId}")]
        public async Task<IActionResult> ManageAsync(string userId)
        {
            var result = await _roleService.ManageAsync(userId);
            return Ok(result);
        }

        [HttpPut("manage/{userId}/{role}")]
        public async Task<IActionResult> ManageRoleAsync(string userId, string role)
        {
            await _roleService.ManageRoleAsync(userId, role);
            return Ok(new { Message = "Role updated successfully." });
        }
    }
}
