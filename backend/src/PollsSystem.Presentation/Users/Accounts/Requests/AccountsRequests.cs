namespace PollsSystem.Presentation.Users.Accounts.Requests;

public record ChangePasswordRequest(Guid UserGid, string CurrentPassword, string Password);

public record ChangeUserNameRequest(string CurrentUserName, string UserName);

public record DeleteAccountRequest(Guid UserGid);

public record RevokeTokenRequest(Guid UserGid);

public record ValidatePasswordRequest(Guid UserGid, string Password);

public record SignInRequest(string UserName, string Password);

public record SignUpRequest(string FirstName, string LastName, string UserName, string Password, Guid? RoleGid);