using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

public interface IProjectRepository : IRepository<Project>
{
    // Project-specific methods
    Task<IEnumerable<Project>> GetProjectsByUserAsync(string userId);
}