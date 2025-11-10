using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using YourProjectName.Models;

namespace TaskManagerApi.Data
{
    public class TaskManagerContext :IdentityDbContext<ApplicationUser>
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
