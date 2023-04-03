namespace PollsSystem.Presentation.Users.UsersManagement.Requests;

public record DeleteUserRequest(string UserGid);

public record GetUserQuery(string UserGid);

public record GetUserByInputNameQuery(string InputName);

