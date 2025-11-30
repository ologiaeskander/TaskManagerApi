using TaskManagerApi.Data;
using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

public class ProjectRepository : Repository<ProjectModel>, IProjectRepository
{
    public ProjectRepository(TaskManagerContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProjectModel>> GetProjectsByUserAsync(string userId)
    {
        return await GetAllAsync(
            filter: p => p.CreatorId == userId,
            includeProperties: "Creator"
        );
    }
}