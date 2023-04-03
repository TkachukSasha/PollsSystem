using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangePollDescriptionValidator : AbstractValidator<ChangePollDescription>
{
    public ChangePollDescriptionValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.Description)
            .MinimumLength(64)
            .MaximumLength(564)
            .NotNull();
    }
}

public sealed record ChangePollDescription(string PollGid, string Description) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangePollDescriptionValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangePollDescriptionHandler : ICommandHandler<ChangePollDescription, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;

    public ChangePollDescriptionHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(ChangePollDescription command, CancellationToken cancellationToken)
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

        return poll.Gid;
    }
}
