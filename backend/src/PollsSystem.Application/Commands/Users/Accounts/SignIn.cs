using FluentValidation;
using Mediator;
using PollsSystem.Application.Responses.Users;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Security.Cryptography;
using PollsSystem.Shared.Security.Providers;
using PollsSystem.Shared.Security.Storage;

namespace PollsSystem.Application.Commands.Users.Accounts;

public class SignInValidator : AbstractValidator<SignIn>
{
    public SignInValidator()
    {
        RuleFor(x => x.UserName)
           .MinimumLength(8)
           .MaximumLength(16)
           .NotEmpty();

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MaximumLength(16)
            .NotEmpty();
    }
}

public sealed record SignIn(string UserName, string Password) : ICommand;

public class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IBaseRepository _baseRepository;
    private readonly IJwtTokenProvider _jwtProvider;
    private readonly IPasswordManager _passwordManager;
    private readonly IStorage _storage;

    public SignInHandler(
        IBaseRepository baseRepository,
        IJwtTokenProvider jwtProvider,
        IPasswordManager passwordManager,
        IStorage storage)
    {
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        _passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public async ValueTask<Unit> Handle(SignIn command, CancellationToken cancellationToken)
    {
        var existingUser = await _baseRepository.GetByConditionAsync<User>(x => x.UserName == command.UserName);

        if (existingUser is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"User with username: {command.UserName} not found");

        var existingRole = await _baseRepository.GetByConditionAsync<Role>(x => x.Gid == existingUser.RoleGid);

        if (existingRole is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
               $"Role with username: {existingUser.RoleGid} not found");

        bool isCorrectPassword = _passwordManager.Validate(command.Password, existingUser.Password);

        if (!isCorrectPassword)
            throw new BaseException(ExceptionCodes.ValueMissmatch,
                $"Password: {command.Password} is incorrect");

        var jwt = _jwtProvider.CreateToken(
             existingUser.Gid.ToString(),
             existingRole.Name,
             existingUser.FirstName,
             existingUser.LastName,
             null
        );

        var authResponse = new AuthResponse(
             existingUser.Gid,
             existingUser.FirstName,
             existingUser.LastName,
             jwt.AccessToken,
             jwt.Expires
        );

        _storage.Set(authResponse, "auth");

        return Unit.Value;
    }
}
