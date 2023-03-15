using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Queries.Polls.Holders;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.Repositories;
using PollsSystem.Presentation;
using PollsSystem.Presentation.Polls.Holders.Requests;
using PollsSystem.Presentation.Polls.PollsManagement;
using PollsSystem.Shared.Dal.Pagination;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Utils.Algorithms;

namespace PollsSystem.Api.Controllers.Polls;

[Authorize]
[Route("api/polls")]
public class PollsController : BaseController
{
    private readonly IBaseRepository _repository;
    private readonly IQuestionRepository _questionRepository;

    public PollsController(
        IMediator mediator,
        IBaseRepository repository,
        IQuestionRepository questionRepository)
        : base(mediator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetPolls([FromQuery] PaginationData pagination)
    {
        var filter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

        var polls = await _repository.GetListAsync<Poll>(filter);

        polls?.Items?.Select(x => x.ToPollResponse());

        return Ok(polls);
    }

    [HttpGet("scores")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetScores()
    {
        var scores = await _repository.GetListAsync<Score>();

        return Ok(scores);
    }

    [AllowAnonymous]
    [HttpGet("poll")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetPoll([FromQuery] GetPollQuery query)
    {
        var poll = await _repository.GetByConditionAsync<Poll>(x => x.Gid == query.PollGid);

        return Ok(poll);
    }

    [AllowAnonymous]
    [HttpGet("by-title")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetPoll([FromQuery] GetPollByTitleQuery query)
    {
        var poll = await _repository.GetByConditionAsync<Poll>(x => x.Title == query.Title);

        return Ok(poll);
    }

    [HttpGet("key")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetPollKey([FromQuery] GetPollQuery query)
    {
        var poll = await _repository.GetByConditionAsync<Poll>(x => x.Gid == query.PollGid);

        return Ok(poll);
    }

    [HttpGet("check-key")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> CheckPollKey([FromQuery] CheckPollKey query, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(query, cancellationToken));
    }

    [HttpGet("questions")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> CheckPollQuestions([FromQuery] GetPollQuery query)
    {
        var questions = await _questionRepository.GetPollQuestionsWithAnswersAsync(query.PollGid);

        questions.ToList().ShuffleQuizQuestionsV1();

        return Ok(questions);
    }
}
