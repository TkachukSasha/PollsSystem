namespace PollsSystem.Presentation.Users.UsersManagement.Requests;

public record DeleteUserRequest(Guid UserGid);

public record GetUserQuery(Guid UserGid);

public record GetUserByInputNameQuery(string InputName);

