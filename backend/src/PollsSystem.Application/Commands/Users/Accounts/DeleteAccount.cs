using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
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

public sealed record DeleteAccount(string UserGid) : ICommand<bool>, IValidate
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

public class DeleteAccountHandler : BaseCommandHandler<DeleteAccount, bool>
{
    public DeleteAccountHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(DeleteAccount command, CancellationToken cancellationToken)
    {
        var userGid = Guid.Parse(command.UserGid);

        var existingUser = await _baseRepository.GetByConditionAsync<User>(x => x.Gid == userGid);

        if (existingUser is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"User: {command.UserGid} is null or empty");

        var userResultGid = _baseRepository.Delete<User>(existingUser.Gid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return userResultGid != Guid.Empty ? true : false;
    }
}
