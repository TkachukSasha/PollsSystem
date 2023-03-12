namespace PollsSystem.Application.Responses.Users;

public record UserExtendedResponse(Guid Gid, string FirstName, string LastName, string UserName, string Role);
public record UserResponse(Guid Gid, string FirstName, string LastName, string UserName);
public record RoleResponse(Guid Gid, string Name);

public record AuthResponse(Guid UserGid, string FirstName, string LastName, string AccessToken, long Expiry);