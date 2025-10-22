using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Data
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dummy Data Users
            modelBuilder.Entity<User>().HasData(
                new User { 
                    Id = 1, 
                    FullName = "Youssef Nabil",
                    Username = "youssef",
                    Password = "hashed123",
                    Email = "youssef@example.com",
                    Phone = 1234567890,
                    Role = Role.Manager,
                    Department = Department.Technical
                },
                new User { 
                    Id = 2,
                    FullName = "Sara Magued",
                    Username = "sara",
                    Password = "hashed456",
                    Email = "sara@example.com",
                    Phone = 987654321,
                    Role = Role.Employee,
                    Department = Department.Marketing
                }
            );

            // Dummy Data Projects
            modelBuilder.Entity<Project>().HasData(
                new Project {
                    Id = 1,
                    Name = "Website Revamp",
                    Description = "Update company website and UI",
                    CreatedBy = 1,
                    CreatedAt = new DateTime(2025, 10, 22, 15, 45, 50, 0, DateTimeKind.Utc)
                },
                new Project {
                    Id = 2,
                    Name = "Mobile App Launch",
                    Description = "Develop a mobile app for our service",
                    CreatedBy = 1,
                    CreatedAt = new DateTime(2025, 10, 22, 15, 45, 50, 0, DateTimeKind.Utc)
                }
            );

            // Dummy Data Jobs
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    Id = 1,
                    Title = "Redesign Homepage",
                    Description = "Improve layout and usability of homepage",
                    Status = Status.InProgress,
                    Priority = Priority.High,
                    AssignedToUserId = 2,
                    ProjectId = 1,
                    CreatedAt = new DateTime(2025, 10, 17, 15, 45, 50, 0, DateTimeKind.Utc),
                    DueDate = new DateTime(2025, 10, 25, 15, 45, 50, 0, DateTimeKind.Utc),
                    CreatorId = 1
                },
                new Job
                {
                    Id = 2,
                    Title = "App API Integration",
                    Description = "Connect mobile app to backend API",
                    Status = Status.ToDo,
                    Priority = Priority.Medium,
                    AssignedToUserId = 2,
                    ProjectId = 2,
                    CreatedAt = new DateTime(2025, 10, 20, 15, 45, 50, 0, DateTimeKind.Utc),
                    DueDate = new DateTime(2025, 11, 1, 15, 45, 50, 0, DateTimeKind.Utc),
                    CreatorId = 1
                },
                new Job
                {
                    Id = 3,
                    Title = "Database Backup",
                    Description = "Schedule automated backups",
                    Status = Status.Done,
                    Priority = Priority.Low,
                    AssignedToUserId = 1,
                    ProjectId = null, // rogue/general job
                    CreatedAt = new DateTime(2025, 10, 12, 15, 45, 50, 0, DateTimeKind.Utc),
                    DueDate = null,
                    CreatorId = 1
                }
            );
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
