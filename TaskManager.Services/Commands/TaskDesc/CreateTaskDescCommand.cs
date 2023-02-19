using MediatR;

namespace TaskManager.Services.Commands.TaskDesc;

/// <summary>
/// Command to create new task desc
/// </summary>
public record CreateTaskDescCommand : IRequest<Guid>;