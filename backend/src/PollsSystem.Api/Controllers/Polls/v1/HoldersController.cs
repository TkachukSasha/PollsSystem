using Mapster;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Commands.Polls.Holder;
using PollsSystem.Presentation.Polls.Holders.Requests;

namespace PollsSystem.Api.Controllers.Polls.v1;

[Authorize]
[Route("api/v{version:apiVersion}/holders")]
[ApiVersion("1.0")]
public class HoldersController : BaseController
{
    public HoldersController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost("create-poll")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreatePoll([FromBody] CreatePollRequest request, CancellationToken cancellationToken)
    {
        CreatePoll command = request.Adapt<CreatePoll>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPost("create-poll-questions")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreatePollQuestions([FromBody] CreatePollQuestionsRequest request, CancellationToken cancellationToken)
    {
        CreatePollQuestions command = request.Adapt<CreatePollQuestions>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-poll-description")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangePollDescription([FromBody] ChangePollDescriptionRequest request, CancellationToken cancellationToken)
    {
        ChangePollDescription command = request.Adapt<ChangePollDescription>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-poll-duration")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangePollDuration([FromBody] ChangePollDurationRequest request, CancellationToken cancellationToken)
    {
        ChangePollDuration command = request.Adapt<ChangePollDuration>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-poll-key")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangePollKey([FromBody] ChangePollKeyRequest request, CancellationToken cancellationToken)
    {
        ChangePollKey command = request.Adapt<ChangePollKey>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-poll-title")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangePollTitle([FromBody] ChangePollTitleRequest request, CancellationToken cancellationToken)
    {
        ChangePollTitle command = request.Adapt<ChangePollTitle>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-question-text")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangeQuestionText([FromBody] ChangeQuestionTextRequest request, CancellationToken cancellationToken)
    {
        ChangeQuestionText command = request.Adapt<ChangeQuestionText>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-answer-text")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangeAnswerText([FromBody] ChangeAnswerTextRequest request, CancellationToken cancellationToken)
    {
        ChangeAnswerText command = request.Adapt<ChangeAnswerText>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-answer-score")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangeAnswerScore([FromBody] ChangeAnswerScoreRequest request, CancellationToken cancellationToken)
    {
        ChangeAnswerScore command = request.Adapt<ChangeAnswerScore>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("delete-poll")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeletePoll([FromBody] DeletePollRequest request, CancellationToken cancellationToken)
    {
        DeletePoll command = request.Adapt<DeletePoll>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("delete-poll-question")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeletePollQuestion([FromBody] DeletePollQuestionRequest request, CancellationToken cancellationToken)
    {
        DeletePollQuestion command = request.Adapt<DeletePollQuestion>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("delete-question-answer")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteQuestionAnswer([FromBody] DeleteQuestionAnswerRequest request, CancellationToken cancellationToken)
    {
        DeleteQuestionAnswer command = request.Adapt<DeleteQuestionAnswer>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }
}
