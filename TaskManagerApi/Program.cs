using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using Microsoft.AspNetCore.Identity;
using YourProjectName.Models;
using Swashbuckle.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TaskManagerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TaskManagerContext>();

var app = builder.Build();

// And these middlewares (order matters!)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();           // ? Enables Swagger JSON generation
    app.UseSwaggerUI();         // ? Enables the Swagger UI
}

app.UseHttpsRedirection();
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
