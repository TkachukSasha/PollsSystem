using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangePollTitleValidator : AbstractValidator<ChangePollTitle>
{
    public ChangePollTitleValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.Title)
            .MinimumLength(8)
            .NotNull();
    }
}

public sealed record ChangePollTitle(string PollGid, string Title) : ICommand<bool>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangePollTitleValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangePollTitleHandler : BaseCommandHandler<ChangePollTitle, bool>
{
    public ChangePollTitleHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(ChangePollTitle command, CancellationToken cancellationToken)
    {
        var isTitleUnique = await _baseRepository.IsFieldUniqueAsync<Poll>(x => x.Title == command.Title);

        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == Guid.Parse(command.PollGid));

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.Title} is null!");

        var poll = Poll.ChangePollTitle(
               existingPoll,
               command.Title,
               isTitleUnique.GetValueOrDefault()
           );

        _baseRepository.Update(poll);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return poll.Title == command.Title ? true : false;
    }
}

