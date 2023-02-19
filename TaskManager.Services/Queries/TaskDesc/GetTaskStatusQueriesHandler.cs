using MediatR;
using TaskManager.Services.DTOs;

namespace TaskManager.Services.Queries.TaskDesc;

/// <summary>
/// Handle queries associated with TaskDesc
/// </summary>
public record GetTaskStatusQueriesHandler(Guid Id) : IRequest<TaskDescStatusDto>; 