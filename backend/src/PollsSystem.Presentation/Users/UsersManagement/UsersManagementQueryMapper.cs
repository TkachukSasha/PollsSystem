using PollsSystem.Application.Responses.Users;
using PollsSystem.Domain.Entities.Users;

namespace PollsSystem.Presentation.Users.UsersManagement;

public static class UsersManagementQueryMapper
{
    public static UserExtendedResponse ToUserExtendedResponse(this User user, string role)
      => new UserExtendedResponse(
          user.Gid,
          user.FirstName.Value,
          user.LastName.Value,
          user.UserName.Value,
          role
      );

    public static UserResponse ToUserResponse(this User user)
       => new UserResponse(
           user.Gid,
           user.FirstName.Value,
           user.LastName.Value,
           user.UserName.Value
       );
}