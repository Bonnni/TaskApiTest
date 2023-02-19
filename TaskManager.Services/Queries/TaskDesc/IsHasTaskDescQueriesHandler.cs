using MediatR;

namespace TaskManager.Services.Queries.TaskDesc;

/// <summary>
/// Handle queries associated with TaskDesc
/// </summary>
public record IsHasTaskDescQueriesHandler(Guid Id) : IRequest<bool>; 