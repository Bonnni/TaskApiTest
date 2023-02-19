using MediatR;

namespace TaskManager.Services.Queries.TaskDesc;

/// <summary>
/// Handle queries associated with TaskDesc
/// </summary>
public record RunEmptyTaskFromTaskDescQueryHandler(Guid Id) : IRequest<Unit>;