using TaskManagerApi.Data;
using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

public class JobRepository : Repository<Job>, IJobRepository
{
    public JobRepository(TaskManagerContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Job>> GetJobsByStatusAsync(Status status)
    {
        return await GetAllAsync(
            filter: j => j.Status == status,
            includeProperties: "AssignedToUser,Project,Creator"
        );
    }

    public async Task<IEnumerable<Job>> GetJobsByUserAsync(string userId)
    {
        return await GetAllAsync(
            filter: j => j.AssignedToUserId == userId || j.CreatorId == userId,
            includeProperties: "AssignedToUser,Project,Creator"
        );
    }

    public async Task<IEnumerable<Job>> GetOverdueJobsAsync()
    {
        return await GetAllAsync(
            filter: j => j.DueDate.HasValue && j.DueDate.Value < DateTime.Now && j.Status != Status.Done,
            includeProperties: "AssignedToUser,Project,Creator"
        );
    }
}