using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Repositories.Interfaces;

namespace TaskManagerApi.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TaskManagerContext _context;

        public UserRepository(
            TaskManagerContext context,
            UserManager<ApplicationUser> userManager
        ) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<ApplicationUser>> SearchAsync(string keyword)
        {
            return await _context.Users
                .Where(u => u.Email.Contains(keyword)
                         || u.FirstName.Contains(keyword)
                         || u.LastName.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetByRoleAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }
    }

}
