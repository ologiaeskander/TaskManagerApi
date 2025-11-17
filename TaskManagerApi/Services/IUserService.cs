using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
    }
}
