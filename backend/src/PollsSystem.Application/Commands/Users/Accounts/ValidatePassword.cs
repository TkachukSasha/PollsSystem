using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Security.Cryptography;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Users.Accounts;

public class ValidatePasswordValidator : AbstractValidator<ValidatePassword>
{
    public ValidatePasswordValidator()
    {
        RuleFor(x => x.UserGid)
            .NotNull();

        RuleFor(x => x.Password)
            .NotNull();
    }
}

public sealed record ValidatePassword(Guid UserGid, string Password) : ICommand<bool>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ValidatePasswordValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ValidatePasswordHandler : ICommandHandler<ValidatePassword, bool>
{
    private readonly IBaseRepository _baseRepository;
    private readonly IPasswordManager _passwordManager;

    public ValidatePasswordHandler(
        IBaseRepository baseRepository,
        IPasswordManager passwordManager)
    {
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        _passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
    }

    public async ValueTask<bool> Handle(ValidatePassword command, CancellationToken cancellationToken)
    {
        var user = await _baseRepository.GetByConditionAsync<User>(x => x.Gid == command.UserGid);

        if (user is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"User with: {command.UserGid} is null!");

        return _passwordManager.Validate(command.Password, user.Password);
    }
}


