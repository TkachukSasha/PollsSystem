using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

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

public sealed record ChangePollKey(Guid PollGid, string CurrentKey) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangePollKeyValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangePollKeyHandler : ICommandHandler<ChangePollKey, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;

    public ChangePollKeyHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(ChangePollKey command, CancellationToken cancellationToken)
    {
        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == command.PollGid);

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        var poll = Poll.RegenerateKey(
               existingPoll,
               command.CurrentKey
        );

        _baseRepository.Update(poll);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return poll.Gid;
    }
}
