using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApi.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IActionResult> ManageAsync(string userId);
        Task ManageRoleAsync(string userId, string role);
    }
}
