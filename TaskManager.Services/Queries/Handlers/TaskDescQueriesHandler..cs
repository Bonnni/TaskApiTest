using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Services.DTOs;
using TaskManager.Services.Queries.TaskDesc;
using TaskManagerApi.Model;

namespace TaskManager.Services.Queries.Handlers;

/// <summary>
/// Handle commands associated with TaskDescEntity
/// </summary>
public class TaskManagerCommandsHandler : ServiceBase<TaskManagerDbContext>, 
    IRequestHandler<IsHasTaskDescQueriesHandler, bool>,
    IRequestHandler<GetTaskStatusQueriesHandler, TaskDescStatusDto>
{
    public TaskManagerCommandsHandler(TaskManagerDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<bool> Handle(IsHasTaskDescQueriesHandler request, CancellationToken cancellationToken)
        => await Context.TaskEntities.AnyAsync(x => x.Id == request.Id, cancellationToken);

    public async Task<TaskDescStatusDto> Handle(GetTaskStatusQueriesHandler request, CancellationToken cancellationToken)
    {
        var task = await Context.TaskEntities
            .FirstAsync(x => x.Id == request.Id, cancellationToken);

        return Mapper.Map<TaskDescStatusDto>(task);
    }
}