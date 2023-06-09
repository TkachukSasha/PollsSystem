﻿using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangePollKeyValidator : AbstractValidator<ChangePollKey>
{
    public ChangePollKeyValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.CurrentKey)
            .NotNull();
    }
}

public sealed record ChangePollKey(string PollGid, string CurrentKey) : ICommand<bool>;

public class ChangePollKeyHandler : BaseCommandHandler<ChangePollKey, bool>
{
    public ChangePollKeyHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(ChangePollKey command, CancellationToken cancellationToken)
    {
        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == Guid.Parse(command.PollGid));

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        var key = Guid.NewGuid().ToString("N");

        var poll = Poll.RegenerateKey(
               existingPoll,
               key
        );

        _baseRepository.Update(poll);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
