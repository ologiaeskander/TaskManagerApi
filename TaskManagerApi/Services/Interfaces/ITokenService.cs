using TaskManagerApi.Models;

namespace TaskManagerApi.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(ApplicationUser user);
    }
}
