using PollsSystem.Application.Responses.Users;
using PollsSystem.Domain.Entities.Users;

namespace PollsSystem.Presentation.Users.Roles;

public static class RolesQueryMapper
{
    public static RoleResponse ToRoleResponse(this Role role)
      => new RoleResponse(
          role.Gid,
          role.Name
      );
}
