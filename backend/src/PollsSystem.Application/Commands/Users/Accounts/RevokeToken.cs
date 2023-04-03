using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Application.Responses.Users;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Security.Providers;
using PollsSystem.Shared.Security.Storage;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Users.Accounts;

public class RevokeTokenValidator : AbstractValidator<RevokeToken>
{
    public RevokeTokenValidator()
    {
        RuleFor(x => x.UserGid)
            .NotNull();
    }
}

public sealed record RevokeToken(string UserGid) : ICommand, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new RevokeTokenValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class RevokeTokenHandler : ICommandHandler<RevokeToken>
{
    private readonly IBaseRepository _baseRepository;
    private readonly IJwtTokenProvider _jwtProvider;
    private readonly IStorage _storage;

    public RevokeTokenHandler(
        IBaseRepository baseRepository,
        IJwtTokenProvider jwtProvider,
        IStorage storage)
    {
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public async ValueTask<Unit> Handle(RevokeToken command, CancellationToken cancellationToken)
    {
        var userGid = Guid.Parse(command.UserGid);

        var existingUser = await _baseRepository.GetByConditionAsync<User>(x => x.Gid == userGid);

        var existingRole = await _baseRepository.GetByConditionAsync<Role>(x => x.Gid == existingUser.RoleGid);

        if (existingUser is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"User: {command.UserGid} is null or empty");

        if (existingRole is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Role: {existingUser.RoleGid} is null");

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
