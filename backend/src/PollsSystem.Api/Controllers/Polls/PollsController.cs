using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Queries.Polls.Holders;
using PollsSystem.Application.Responses.Polls;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.Repositories;
using PollsSystem.Presentation.Polls.Holders.Requests;
using PollsSystem.Presentation.Polls.PollsManagement;
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetPolls()
    {
        var polls = await _repository.GetListAsync<Poll>();

        var response = polls.Select(x => x?.ToPollResponse()).ToList();

        return response is null ? NoContent() : Ok(response);
    }

    [HttpGet("user-polls")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetUserPolls([FromQuery] GetUserPollsQuery query)
    {
        var userGid = Guid.Parse(query.UserGid);

        var polls = await _repository.GetEntitiesByConditionAsync<Poll>(x => x.AuthorGid == userGid);

        var response = polls.Select(x => x?.ToPollResponse()).ToList();

        return response is null ? NoContent() : Ok(response);
    }

    [HttpGet("scores")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetScores()
    {
        var scores = await _repository.GetListAsync<Score>();

        return Ok(scores);
    }

    [HttpGet("key")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetPollKey([FromQuery] GetPollQuery query)
    {
        var pollGid = Guid.Parse(query.PollGid);

        var poll = await _repository.GetByConditionAsync<Poll>(x => x.Gid == pollGid);

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
    public async ValueTask<IActionResult> GetPollQuestions([FromQuery] GetPollQuery query)
    {
        var pollGid = Guid.Parse(query.PollGid);

        var questions = await _questionRepository.GetPollQuestionsWithAnswersAsync(pollGid);

        questions.ToList().ShuffleQuizQuestionsV1();

        var responseList = new List<QuestionsWithAnswersResponse>();

        List<Question> questionsList = questions.ToList();
        List<Answer> answers = questionsList.SelectMany(q => q.Answers).ToList();

        foreach (var question in questionsList)
        {
            var questionAnswers = answers.Where(a => a.QuestionGid == question.Gid).ToList();
            responseList.Add(question.ToQuestionsWithAnswersResponse(questionAnswers));
        }

        return Ok(responseList);
    }
}
