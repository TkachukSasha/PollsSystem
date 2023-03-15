using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using PollsSystem.Shared.Security.Cryptography;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Users.Accounts;

public class ChangePasswordValidator : AbstractValidator<ChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.UserGid)
            .NotNull();

        RuleFor(x => x.CurrentPassword)
            .NotNull();

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MaximumLength(16)
            .NotNull();
    }
}

public sealed record ChangePassword(Guid UserGid, string CurrentPassword, string Password) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangePasswordValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangePasswordHandler : ICommandHandler<ChangePassword, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;
    private readonly IPasswordManager _passwordManager;

    public ChangePasswordHandler(
       IUnitOfWork unitOfWork,
       IPasswordManager passwordManager,
       IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(ChangePassword command, CancellationToken cancellationToken)
    {
        var existingUser = await _baseRepository.GetByConditionAsync<User>(x => x.Gid == command.UserGid);

        if (existingUser is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"User: {command.UserGid} is null or empty");

        bool isPasswordValid = _passwordManager.Validate(command.CurrentPassword, existingUser.Password);

        if (!isPasswordValid)
            throw new BaseException(ExceptionCodes.ValueMissmatch,
                $"Password: {command.CurrentPassword} is invalid");

        string passwordHash = _passwordManager.Secure(command.Password);

        var user = User.ChangeUserPassword(
            existingUser,
            passwordHash
        );

        _baseRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Gid;
    }
}
