using Mapster;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Commands.Statistics.Results;
using PollsSystem.Application.Queries.Statistics.Results;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Presentation.Statistics.Results;
using PollsSystem.Presentation.Statistics.Results.Requests;
using PollsSystem.Shared.Dal.Repositories;

namespace PollsSystem.Api.Controllers.Statistics;

[Authorize]
[Route("api/statistics")]
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

    [HttpGet("calcultions")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetCalculations([FromQuery] GetCalculations query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);

        return response is null ? NoContent() : Ok(response);
    }

    [HttpGet("results")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetResults([FromQuery] GetResultsQuery query)
    {
        var results = await _repository.GetEntitiesByConditionAsync<Result>(x => x.PollGid == query.PollGid);

        var response = results?.Select(x => x?.ToResultResponse());

        return results is null ? NoContent() : Ok(response?.OrderBy(x => x?.Percents));
    }

    [HttpGet("result")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetResult([FromQuery] GetResultsByLastNameQuery query)
    {
        var result = await _repository.GetByConditionAsync<Result>(x => x.PollGid == query.PollGid && x.LastName == query.LastName);

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
