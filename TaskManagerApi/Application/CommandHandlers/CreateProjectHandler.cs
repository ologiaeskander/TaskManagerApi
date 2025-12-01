using MediatR;
using TaskManagerApi.Models;
using TaskManagerApi.Application.Commands;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class CreateProjectHandler
    : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IProjectRepository _projectRepo;
    private readonly IUserContext _userContext;

    public CreateProjectHandler(
        IProjectRepository projectRepo,
        IUserContext userContext)
    {
        _projectRepo = projectRepo;
        _userContext = userContext;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken ct)
    {
        var userId = _userContext.UserId; // should be automatically pulled

        var project = new ProjectModel
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            CreatorId = userId
        };

        await _projectRepo.AddAsync(project);
        await _projectRepo.SaveChangesAsync();

        return project.Id;
    }
}
