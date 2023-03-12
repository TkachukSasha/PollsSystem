namespace PollsSystem.Presentation.Users.Accounts.Requests;

public record SignUpRequest(string FirstName, string LastName, string UserName, string Password, Guid? RoleGid);