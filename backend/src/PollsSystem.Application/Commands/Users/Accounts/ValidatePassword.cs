﻿using FluentValidation;
using Mediator;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Security.Cryptography;

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

public sealed record ValidatePassword(string UserGid, string Password) : ICommand<bool>;

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
        var userGid = Guid.Parse(command.UserGid);

        var user = await _baseRepository.GetByConditionAsync<User>(x => x.Gid == userGid);

        if (user is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"User with: {command.UserGid} is null!");

        return _passwordManager.Validate(command.Password, user.Password);
    }
}


