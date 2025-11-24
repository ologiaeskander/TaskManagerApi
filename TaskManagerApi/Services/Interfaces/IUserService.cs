using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using RegisterModel = TaskManagerApi.Models.RegisterModel;

namespace TaskManagerApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> LoginAsync(TokenRequestModel model);
    }
}
