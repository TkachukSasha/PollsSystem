namespace PollsSystem.Presentation.Users.Roles.Requests;

public record CreateRoleRequest(string Name);

public record ChangeRoleNameRequest(string CurrentName, string Name);

public record DeleteRoleRequest(Guid RoleGid);

public record GetRoleByNameQuery(string Name);
