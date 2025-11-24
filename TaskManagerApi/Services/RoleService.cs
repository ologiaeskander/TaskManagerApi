using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManagerApi.Models;
using TaskManagerApi.Services.Interfaces;

namespace TaskManagerApi.Services
{
    [Authorize(Roles = "Administrator")]
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public RoleService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<IActionResult> ManageAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new NotFoundObjectResult($"User with ID {userId} not found.");
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var manageRolesModel = new
            {
                UserId = user.Id,
                UserName = user.UserName,
                AssignedRoles = userRoles,
                AvailableRoles = roles.Select(r => r.Name).Except(userRoles)
            };

            return new OkObjectResult(manageRolesModel);
        }

        public async Task ManageRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }
            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new Exception($"Role {role} does not exist.");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(role))
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
