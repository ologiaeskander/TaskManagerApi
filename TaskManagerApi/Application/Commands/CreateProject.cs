using MediatR;
namespace TaskManagerApi.Application.Commands
{
    public record CreateProjectCommand(
    string Name,
    string? Description
) : IRequest<int>;

}
