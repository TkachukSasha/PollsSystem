﻿using Mapster;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Commands.Statistics.Results;
using PollsSystem.Application.Queries.Statistics.Results;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Presentation.Statistics.Results;
using PollsSystem.Presentation.Statistics.Results.Requests;
using PollsSystem.Shared.Dal.Repositories;

namespace PollsSystem.Api.Controllers.Statistics.v1;

[Authorize]
[Route("api/v{version:apiVersion}/statistics")]
[ApiVersion("1.0")]
public class StatisticsController : BaseController
{
    private readonly IBaseRepository _repository;

    public StatisticsController(
        IMediator mediator,
        IBaseRepository repository)
        : base(mediator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet("calculations")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetCalculations([FromQuery] GetCalculations query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);

        return response is null ? NoContent() : Ok(response);
    }

    [HttpGet("get-result")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetResult([FromQuery] GetResultQuery query)
    {
        var pollGid = Guid.Parse(query.PollGid);

        var result = await _repository.GetByConditionAsync<Result>(x => x.PollGid == pollGid && x.FirstName == query.FirstName && x.LastName == query.LastName);

        return result is null ? Ok(false) : Ok(true);
    }

    [HttpGet("results")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetResults([FromQuery] GetResultsQuery query)
    {
        var pollGid = Guid.Parse(query.PollGid);

        var results = await _repository.GetEntitiesByConditionAsync<Result>(x => x.PollGid == pollGid);

        var response = results?.Select(x => x?.ToResultResponse());

        return results is null ? NoContent() : Ok(response?.OrderByDescending(x => x?.Percents));
    }

    [HttpGet("result")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetResult([FromQuery] GetResultsByLastNameQuery query)
    {
        var pollGid = Guid.Parse(query.PollGid);

        var result = await _repository.GetByConditionAsync<Result>(x => x.PollGid == pollGid && x.LastName == query.LastName);

        result.ToResultResponse();

        return result is null ? NoContent() : Ok(result);
    }

    [HttpPatch("change-score")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangeScore([FromBody] ChangeResultScoreRequest request, CancellationToken cancellationToken)
    {
        ChangeResultScore command = request.Adapt<ChangeResultScore>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("delete-result")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteResult([FromBody] DeleteResultRequest request, CancellationToken cancellationToken)
    {
        DeleteResult command = request.Adapt<DeleteResult>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }
}
