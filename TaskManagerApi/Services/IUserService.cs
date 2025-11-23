using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using RegisterModel = TaskManagerApi.Models.RegisterModel;

namespace TaskManagerApi.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> LoginAsync(TokenRequestModel model);
        Task<IActionResult> ManageAsync(string userId);
        Task ManageRoleAsync(string userId, string role);
    }
}
