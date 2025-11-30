using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

public interface IJobRepository : IRepository<JobModel>
{
    // Job-specific methods
    Task<IEnumerable<JobModel>> GetJobsByStatusAsync(Status status);
    Task<IEnumerable<JobModel>> GetJobsByUserAsync(string userId);
    Task<IEnumerable<JobModel>> GetOverdueJobsAsync();
}