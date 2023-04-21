using FluentValidation;
using Mediator;
using PollsSystem.Application.Responses.Users;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using PollsSystem.Shared.Security.Cryptography;
using PollsSystem.Shared.Security.Providers;
using PollsSystem.Shared.Security.Storage;

namespace PollsSystem.Application.Commands.Users.Accounts;

public class SignUpValidator : AbstractValidator<SignUp>
{
    public SignUpValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(64)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .MaximumLength(64)
            .NotEmpty();

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

public sealed record SignUp(string FirstName, string LastName, string UserName, string Password, Guid? RoleGid) : ICommand;

public class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IJwtTokenProvider _jwtProvider;
    private readonly IStorage _storage;

    public SignUpHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository,
        IPasswordManager passwordManager,
        IJwtTokenProvider jwtProvider,
        IStorage storage)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        _passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
        _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public async ValueTask<Unit> Handle(SignUp command, CancellationToken cancellationToken)
    {
        bool? isFirstNameUnique = await _baseRepository.IsFieldUniqueAsync<User>(x => x.FirstName == command.FirstName);

        bool? isLastNameUnique = await _baseRepository.IsFieldUniqueAsync<User>(x => x.LastName == command.LastName);

        bool? isUserNameUnique = await _baseRepository.IsFieldUniqueAsync<User>(x => x.UserName == command.UserName);

        string securePassword = _passwordManager.Secure(command.Password);

        var validRole = command.RoleGid is not null ? await _baseRepository.GetByConditionAsync<Role>(x => x.Gid == command.RoleGid.GetValueOrDefault())
                                                   : await _baseRepository.GetByConditionAsync<Role>(x => x.Name == Role.DefaultRole);

        var user = User.Init(
                    command.FirstName,
                    command.LastName,
                    command.UserName,
                    securePassword,
                    validRole,
                    isFirstNameUnique,
                    isLastNameUnique,
                    isUserNameUnique
        );

        _baseRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var jwt = _jwtProvider.CreateToken(
            user.Gid.ToString("N"),
            validRole.Name,
            user.FirstName,
            user.LastName,
            null
        );

        var authResponse = new AuthResponse(
            user.Gid,
            user.FirstName,
            user.LastName,
            jwt.AccessToken,
            jwt.Expires
        );

        _storage.Set(authResponse, "auth");

        return Unit.Value;
    }
}
