using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using Microsoft.AspNetCore.Identity;
using YourProjectName.Models;
using TaskManagerApi.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TaskManagerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TaskManagerContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    try
    {
        await DataSeeder.SeedAsync(scope.ServiceProvider);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Seeding failed");
    }
}

// Then your other middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
