using Mapster;
using PollsSystem.Application.Commands.Users;

namespace PollsSystem.Presentation.Users.Accounts.Requests;

public class AccountsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SignUpRequest, SignUp>()
            .RequireDestinationMemberSource(true);
    }
}
