using Mapster;
using PollsSystem.Application.Commands.Users.UsersManagement;

namespace PollsSystem.Presentation.Users.UsersManagement.Requests;

public class UsersManagementMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DeleteUserRequest, DeleteUser>()
            .RequireDestinationMemberSource(true);
    }
}
