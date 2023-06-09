﻿using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

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

public sealed record ChangePollDuration(string PollGid, int Duration) : ICommand<bool>;

public class ChangePollDurationHandler : BaseCommandHandler<ChangePollDuration, bool>
{
    public ChangePollDurationHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(ChangePollDuration command, CancellationToken cancellationToken)
    {
        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == Guid.Parse(command.PollGid));

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        var poll = Poll.ChangePollDuration(
                existingPoll,
                command.Duration
        );

        _baseRepository.Update(poll);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return poll.Duration == command.Duration ? true : false;
    }
}
