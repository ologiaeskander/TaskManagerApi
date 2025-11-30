using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> SearchAsync(string keyword);
        Task<IEnumerable<ApplicationUser>> GetByRoleAsync(string roleName);
    }
}
