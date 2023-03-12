using Carter;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using PollsSystem.Application.Commands.Users;
using PollsSystem.Presentation.Users.Accounts.Requests;

namespace PollsSystem.Presentation.Users.Accounts;

public class AccountsModule : CarterModule
{
    public AccountsModule()
        : base("/accounts")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/sign-up", async (SignUpRequest request, ISender sender) =>
        {
            SignUp command = request.Adapt<SignUp>();

            await sender.Send(command);
        });
    }
}