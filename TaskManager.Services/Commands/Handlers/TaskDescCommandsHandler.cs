using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Services.Commands.TaskDesc;
using TaskManager.Services.Enums;
using TaskManager.Services.Queries.TaskDesc;
using TaskManagerApi.Model;
using TaskManagerApi.Model.Entities;

namespace TaskManager.Services.Commands.Handlers;

/// <summary>
/// Handle queries associated with TaskDescEntity
/// </summary>
public class TaskDescQueriesHandler : ServiceBase<TaskManagerDbContext>,
    IRequestHandler<CreateTaskDescCommand, Guid>,
    IRequestHandler<RunEmptyTaskFromTaskDescQueryHandler>
{
    public TaskDescQueriesHandler(TaskManagerDbContext context, IMapper mapper) : base(context, mapper) { }
    
    public async Task<Guid> Handle( CreateTaskDescCommand request, CancellationToken cancellationToken)
    {
        var taskNew = new TaskDescEntity
        {
            Id = Guid.NewGuid(),
            UpdateTime = DateTime.Now,
            Status = (int)TaskDescEnitityStatus.Created
        };

        await Context.AddAsync(taskNew, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return taskNew.Id;
    }
    
    public Task<Unit> Handle(RunEmptyTaskFromTaskDescQueryHandler request, CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            var taskToRun = Context.TaskEntities.Find(request.Id);
            if (taskToRun == null) throw new($"Entity {typeof(TaskDescEntity)} not found.");

            ChangeTaskDescBody(Context, taskToRun, TaskDescEnitityStatus.Running);
            Thread.Sleep(120000);
            ChangeTaskDescBody(Context, taskToRun, TaskDescEnitityStatus.Finished);
            
        }, cancellationToken);
        
        return Task.FromResult(Unit.Value);
    }

    private static void ChangeTaskDescBody(DbContext context, TaskDescEntity taskDescEntity, TaskDescEnitityStatus descEnitityStatus)
    {
        taskDescEntity.Status = (int)descEnitityStatus;
        taskDescEntity.UpdateTime = DateTime.Now;
        context.SaveChanges();
    }
}