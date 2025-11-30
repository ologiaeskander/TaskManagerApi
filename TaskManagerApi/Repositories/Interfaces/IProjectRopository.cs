using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

public interface IProjectRepository : IRepository<ProjectModel>
{
    // Project-specific methods
    Task<IEnumerable<ProjectModel>> GetProjectsByUserAsync(string userId);
}