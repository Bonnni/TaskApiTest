using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Services.Commands.TaskDesc;
using TaskManager.Services.Queries.TaskDesc;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskManagerController : BaseController
{
    public TaskManagerController(IServiceScopeFactory serviceScopeFactory, IMediator mediator) 
        : base(serviceScopeFactory, mediator){}

    /// <summary>
    /// Created empty task with imitation of work
    /// </summary>
    /// <param name="token"></param>
    /// <returns>task guid</returns>
    /// <response code="202">return task guid</response>
    [HttpGet("task")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Task(CancellationToken token)
    {
        var mediator = ServiceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
        
        var taskGuid = await mediator.Send(new CreateTaskDescCommand(), token);
        
        #pragma warning disable CS4014
        mediator.Send(new RunEmptyTaskFromTaskDescQueryHandler(taskGuid), token);

        return Accepted(taskGuid);
    }

    /// <summary>
    /// Get the status of the task
    /// </summary>
    /// <param name="id"></param>
    /// <response code="404">not found task</response>
    /// <response code="400">id is not a guid</response>
    [HttpPost("task/{id}")]
    public async Task<IActionResult> GetTaskStatus(string id, CancellationToken token)
    {
        if (!Guid.TryParse(id, out var guid)) return BadRequest($"this id - {id} is not a guid");
        
        if (!await Mediator.Send(new IsHasTaskDescQueriesHandler(guid), token)) return NotFound($"Not found task by id {id}");

        return Ok(await Mediator.Send(new GetTaskStatusQueriesHandler(guid), token));
    }
}