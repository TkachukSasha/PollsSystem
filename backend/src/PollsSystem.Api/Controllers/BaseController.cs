using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace PollsSystem.Api.Controllers;

[ApiController]
//[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "latest" })]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseController(IMediator mediator)
        => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
}
