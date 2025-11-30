using TaskManagerApi.Data;
using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

public class JobRepository : Repository<JobModel>, IJobRepository
{
    public JobRepository(TaskManagerContext context) : base(context)
    {
    }

    public async Task<IEnumerable<JobModel>> GetJobsByStatusAsync(Status status)
    {
        return await GetAllAsync(
            filter: j => j.Status == status,
            includeProperties: "AssignedToUser,Project,Creator"
        );
    }

    public async Task<IEnumerable<JobModel>> GetJobsByUserAsync(string userId)
    {
        return await GetAllAsync(
            filter: j => j.AssignedToUserId == userId || j.CreatorId == userId,
            includeProperties: "AssignedToUser,Project,Creator"
        );
    }

    public async Task<IEnumerable<JobModel>> GetOverdueJobsAsync()
    {
        return await GetAllAsync(
            filter: j => j.DueDate.HasValue && j.DueDate.Value < DateTime.Now && j.Status != Status.Done,
            includeProperties: "AssignedToUser,Project,Creator"
        );
    }
}