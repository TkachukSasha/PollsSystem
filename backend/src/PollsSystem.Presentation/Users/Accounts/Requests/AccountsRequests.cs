namespace PollsSystem.Presentation.Users.Accounts.Requests;

public record ChangePasswordRequest(string UserGid, string CurrentPassword, string Password);

public record ChangeUserNameRequest(string CurrentUserName, string UserName);

public record DeleteAccountRequest(string UserGid);

public record RevokeTokenRequest(string UserGid);

public record ValidatePasswordRequest(string UserGid, string Password);

public record SignInRequest(string UserName, string Password);

public record SignUpRequest(string FirstName, string LastName, string UserName, string Password, Guid? RoleGid);