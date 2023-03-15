using Mapster;
using PollsSystem.Application.Commands.Users.Accounts;

namespace PollsSystem.Presentation.Users.Accounts.Requests;

public class AccountsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SignUpRequest, SignUp>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<SignInRequest, SignIn>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<RevokeTokenRequest, RevokeToken>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<ChangeUserNameRequest, ChangeUserName>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<ChangePasswordRequest, ChangePassword>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<DeleteAccountRequest, DeleteAccount>()
            .RequireDestinationMemberSource(true);
    }
}
