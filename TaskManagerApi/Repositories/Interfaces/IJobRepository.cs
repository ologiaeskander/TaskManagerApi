using TaskManagerApi.Data.Repositories;
using TaskManagerApi.Models;

public interface IJobRepository : IRepository<Job>
{
    // Job-specific methods
    Task<IEnumerable<Job>> GetJobsByStatusAsync(Status status);
    Task<IEnumerable<Job>> GetJobsByUserAsync(string userId);
    Task<IEnumerable<Job>> GetOverdueJobsAsync();
}