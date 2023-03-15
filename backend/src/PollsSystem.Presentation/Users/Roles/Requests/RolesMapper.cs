using Mapster;
using PollsSystem.Application.Commands.Users.Roles;

namespace PollsSystem.Presentation.Users.Roles.Requests;

public class RolesMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateRoleRequest, CreateRole>()
             .RequireDestinationMemberSource(true);

        config.NewConfig<ChangeRoleNameRequest, ChangeRoleName>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<DeleteRoleRequest, DeleteRole>()
            .RequireDestinationMemberSource(true);
    }
}