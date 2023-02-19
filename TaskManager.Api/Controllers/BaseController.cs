using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApi.Controllers;

public class BaseController : ControllerBase
{
    protected readonly IServiceScopeFactory ServiceScopeFactory;
    protected readonly IMediator Mediator;
    public BaseController(IServiceScopeFactory serviceScopeFactory, IMediator mediator)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Mediator = mediator;
    }
}