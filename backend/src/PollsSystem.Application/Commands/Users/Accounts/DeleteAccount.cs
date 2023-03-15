using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Users.Accounts;

public class DeleteAccountValidator : AbstractValidator<DeleteAccount>
{
    public DeleteAccountValidator()
    {
        RuleFor(x => x.UserGid)
            .NotNull();
    }
}

public sealed record DeleteAccount(Guid UserGid) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new DeleteAccountValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class DeleteAccountHandler : ICommandHandler<DeleteAccount, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;

    public DeleteAccountHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(DeleteAccount command, CancellationToken cancellationToken)
    {
        var existingUser = await _baseRepository.GetByConditionAsync<User>(x => x.Gid == command.UserGid);

        if (existingUser is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"User: {command.UserGid} is null or empty");

        var userGid = _baseRepository.Delete<User>(existingUser.Gid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return userGid;
    }
}
