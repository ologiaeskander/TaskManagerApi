using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TaskManagerContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        Console.WriteLine("=== STARTING SEEDER ===");

        try
        {
            // Check if users exist
            var userCount = await context.Users.CountAsync();
            Console.WriteLine($"Found {userCount} existing users");

            if (userCount == 0)
            {
                Console.WriteLine("No users found - creating test users...");

                // Create admin user
                var admin = new ApplicationUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };

                var adminResult = await userManager.CreateAsync(admin, "Password123!");
                Console.WriteLine($"Admin creation result: {adminResult.Succeeded}");

                if (!adminResult.Succeeded)
                {
                    Console.WriteLine($"Admin errors: {string.Join(", ", adminResult.Errors.Select(e => e.Description))}");
                }

                // Create regular user
                var user = new ApplicationUser
                {
                    UserName = "user@test.com",
                    Email = "user@test.com",
                    EmailConfirmed = true,
                    FirstName = "Regular",
                    LastName = "User"
                };

                var userResult = await userManager.CreateAsync(user, "Password123!");
                Console.WriteLine($"User creation result: {userResult.Succeeded}");

                if (!userResult.Succeeded)
                {
                    Console.WriteLine($"User errors: {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
                }

                // Verify users were created
                var usersAfter = await context.Users.CountAsync();
                Console.WriteLine($"Users after creation attempt: {usersAfter}");

                if (adminResult.Succeeded && userResult.Succeeded)
                {
                    Console.WriteLine("Seeding projects and jobs...");

                    // Get fresh user instances
                    var freshAdmin = await userManager.FindByEmailAsync("admin@test.com");
                    var freshUser = await userManager.FindByEmailAsync("user@test.com");

                    Console.WriteLine($"Admin ID: {freshAdmin?.Id}");
                    Console.WriteLine($"User ID: {freshUser?.Id}");

                    if (freshAdmin != null && freshUser != null)
                    {
                        var project1 = new Project { Name = "Website Redesign", CreatorId = freshAdmin.Id };
                        var project2 = new Project { Name = "API Development", CreatorId = freshUser.Id };

                        context.Projects.AddRange(project1, project2);
                        var projectsSaved = await context.SaveChangesAsync();
                        Console.WriteLine($"Projects saved: {projectsSaved}");

                        // Get projects with IDs
                        var savedProjects = await context.Projects.ToListAsync();
                        Console.WriteLine($"Projects in database: {savedProjects.Count}");

                        var jobs = new List<Job>
                    {
                        new Job
                        {
                            Title = "Design Homepage",
                            Description = "Create new homepage layout",
                            Status = Status.ToDo,
                            Priority = Priority.High,
                            CreatedAt = DateTime.UtcNow,
                            CreatorId = freshAdmin.Id,
                            ProjectId = savedProjects[0].Id,
                            AssignedToUserId = freshUser.Id
                        },
                        new Job
                        {
                            Title = "Setup Database",
                            Description = "Initialize database schema",
                            Status = Status.InProgress,
                            Priority = Priority.Medium,
                            CreatedAt = DateTime.UtcNow,
                            CreatorId = freshUser.Id,
                            ProjectId = savedProjects[1].Id,
                            AssignedToUserId = freshAdmin.Id
                        }
                    };

                        context.Jobs.AddRange(jobs);
                        var jobsSaved = await context.SaveChangesAsync();
                        Console.WriteLine($"Jobs saved: {jobsSaved}");

                        // Final verification
                        var finalUserCount = await context.Users.CountAsync();
                        var finalProjectCount = await context.Projects.CountAsync();
                        var finalJobCount = await context.Jobs.CountAsync();

                        Console.WriteLine($"Final counts - Users: {finalUserCount}, Projects: {finalProjectCount}, Jobs: {finalJobCount}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Database already has users, skipping seeding.");
            }

            Console.WriteLine("=== SEEDER COMPLETED ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SEEDER FAILED: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }
    public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

        //Seed Default User
        var defaultUser = new ApplicationUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            await userManager.CreateAsync(defaultUser, Authorization.default_password);
            await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
        }
    }
}