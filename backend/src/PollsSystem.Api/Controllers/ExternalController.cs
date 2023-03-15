﻿using Mapster;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Commands.Polls.External;
using PollsSystem.Presentation.Polls.External.Requests;

namespace PollsSystem.Api.Controllers;

[Authorize]
[Route("api/external")]
public class ExternalController : BaseController
{
    public ExternalController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpGet("ping")]
    public string Ping() => "pong";

    [HttpPost("send-replies")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> SendReplies([FromBody] SendRepliesRequest request, CancellationToken cancellationToken)
    {
        SendReplies command = request.Adapt<SendReplies>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }
}
