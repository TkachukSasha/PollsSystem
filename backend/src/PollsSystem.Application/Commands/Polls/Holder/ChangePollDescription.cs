﻿using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangePollDescriptionValidator : AbstractValidator<ChangePollDescription>
{
    public ChangePollDescriptionValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.Description)
            .MinimumLength(8)
            .MaximumLength(564)
            .NotNull();
    }
}

public sealed record ChangePollDescription(string PollGid, string Description) : ICommand<bool>;

public class ChangePollDescriptionHandler : BaseCommandHandler<ChangePollDescription, bool>
{
    public ChangePollDescriptionHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(ChangePollDescription command, CancellationToken cancellationToken)
    {
        var isDesciptionUnique = await _baseRepository.IsFieldUniqueAsync<Poll>(x => x.Description == command.Description);

        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == Guid.Parse(command.PollGid));

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        var poll = Poll.ChangePollDescription(
                existingPoll,
                command.Description,
                isDesciptionUnique.GetValueOrDefault()
            );

        _baseRepository.Update(poll);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return poll.Description == command.Description ? true : false;
    }
}
