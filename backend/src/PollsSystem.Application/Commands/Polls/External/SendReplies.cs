using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Application.Common.Channels;
using PollsSystem.Application.Dto;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using PollsSystem.Utils.Statistics;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.External;

public class SendRepliesValidator : AbstractValidator<SendReplies>
{
    public SendRepliesValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.FirstName)
            .NotNull();

        RuleFor(x => x.LastName)
            .NotNull();
    }
}

public sealed record SendReplies(string PollGid, string FirstName, string LastName, Dictionary<string, string> QuestionAnswer) : ICommand<bool>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new SendRepliesValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class SendRepliesHandler : ICommandHandler<SendReplies, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISendRepliesChannel _channel;
    private readonly IBaseRepository _baseRepository;

    public SendRepliesHandler(
        IUnitOfWork unitOfWork,
        ISendRepliesChannel channel,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<bool> Handle(SendReplies command, CancellationToken cancellationToken)
    {
        List<double> scores = new();

        var pollGid = Guid.Parse(command.PollGid);

        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == pollGid);

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll: {command.PollGid} not found");

        foreach (var (key, value) in command.QuestionAnswer)
        {
            var answer = await _baseRepository.GetByConditionAsync<Answer>(x => x.QuestionGid == Guid.Parse(key) && x.Gid == Guid.Parse(value));

            var score = await _baseRepository.GetByConditionAsync<Score>(x => x.Gid == answer.ScoreGid);

            scores.Add(score.ScoreValue);
        }

        double finalScore = scores.Sum();

        var percents = finalScore.SetPercents(existingPoll.NumberOfQuestions);

        var reply = new UserReply(
            finalScore,
            percents,
            command.FirstName,
            command.LastName,
            existingPoll.Gid
        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _channel.AppyResultAsync(reply, cancellationToken);

        return existingPoll.Gid != Guid.Empty ? true : false;
    }
}
