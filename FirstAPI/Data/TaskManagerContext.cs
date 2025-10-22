using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using Job = TaskManager.Models.Job;

namespace TaskManager.Data
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options):base(options) { }
        public DbSet<Job> Jobs { get; set; }
    }
}
