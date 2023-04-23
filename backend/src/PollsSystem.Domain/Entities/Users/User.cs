using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.ValueObjects.Users;
using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.Entities.Users;

public sealed class User : Entity
{
    private readonly List<Poll> _polls = new();

    private User(
        InputName firstName,
        InputName lastName,
        InputName userName,
        string password,
        Guid roleGid) : base()
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Password = password;
        RoleGid = roleGid;
    }

    public InputName FirstName { get; private set; }
    public InputName LastName { get; private set; }
    public InputName UserName { get; private set; }
    public string Password { get; private set; } = null!;
    public Guid RoleGid { get; private set; }

    public Role? Role { get; set; }
    public IReadOnlyCollection<Poll> Polls => _polls;

    public static User Init(
        string firstNameRequest,
        string lastNameRequest,
        string userNameRequest,
        string password,
        Role? validRole,
        bool? isUserNameUnique)
    {
        if (!isUserNameUnique.GetValueOrDefault())
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"UserName: {userNameRequest} is already exist");

        var firstName = InputName.Init(firstNameRequest);
        var lastName = InputName.Init(lastNameRequest);
        var userName = InputName.Init(userNameRequest);

        var user = new User(firstName,
                            lastName,
                            userName,
                            password,
                            validRole.Gid);

        return user;
    }

    public static User ChangeUserName(
        User user,
        string userNameRequest,
        bool isUserNameUnique)
    {
        if (!isUserNameUnique)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"UserName: {userNameRequest} is already exist");

        var userName = InputName.Init(userNameRequest);

        user.UserName = userName;

        return user;
    }

    public static User ChangeUserPassword(
        User user,
        string password)
    {
        user.Password = password;

        return user;
    }
}