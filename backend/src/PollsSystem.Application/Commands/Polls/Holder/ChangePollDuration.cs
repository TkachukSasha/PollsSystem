using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangePollDurationValidator : AbstractValidator<ChangePollDuration>
{
    public ChangePollDurationValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.Duration)
            .NotEqual(0)
            .NotNull();
    }
}

public sealed record ChangePollDuration(Guid PollGid, int Duration) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangePollDurationValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangePollDurationHandler : ICommandHandler<ChangePollDuration, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;

    public ChangePollDurationHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(ChangePollDuration command, CancellationToken cancellationToken)
    {
        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == command.PollGid);

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        var poll = Poll.ChangePollDuration(
                existingPoll,
                command.Duration
        );

        _baseRepository.Update(poll);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return poll.Gid;
    }
}
