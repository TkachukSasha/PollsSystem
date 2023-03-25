using Mapster;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Commands.Users.Accounts;
using PollsSystem.Application.Responses.Users;
using PollsSystem.Presentation.Users.Accounts.Requests;
using PollsSystem.Shared.Security.Storage;

namespace PollsSystem.Api.Controllers.Users;

[Authorize]
[Route("api/accounts")]
public class AccountsController : BaseController
{
    private readonly IStorage _storage;

    public AccountsController(
        IMediator mediator,
        IStorage storage)
        : base(mediator)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async ValueTask<IActionResult> SignUp([FromBody] SignUpRequest request, CancellationToken cancellationToken)
    {
        SignUp command = request.Adapt<SignUp>();

        await _mediator.Send(command, cancellationToken);

        var data = _storage.Get<AuthResponse>("auth");

        return data is null ? NoContent() : Ok(data);
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> SignIn([FromBody] SignInRequest request, CancellationToken cancellationToken)
    {
        SignIn command = request.Adapt<SignIn>();

        await _mediator.Send(command, cancellationToken);

        var data = _storage.Get<AuthResponse>("auth");

        return data is null ? NoContent() : Ok(data);
    }

    [AllowAnonymous]
    [HttpPatch("revoke-token")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> Revoke([FromBody] RevokeTokenRequest request, CancellationToken cancellationToken)
    {
        RevokeToken command = request.Adapt<RevokeToken>();

        await _mediator.Send(command, cancellationToken);

        var data = _storage.Get<AuthResponse>("auth");

        return data is null ? NoContent() : Ok(data);
    }

    [HttpPatch("validate-password")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ValidatePassword([FromBody] ValidatePasswordRequest request, CancellationToken cancellationToken)
    {
        ValidatePassword command = request.Adapt<ValidatePassword>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-password")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        ChangePassword command = request.Adapt<ChangePassword>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-username")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangeUsername([FromBody] ChangeUserNameRequest request, CancellationToken cancellationToken)
    {
        ChangeUserName command = request.Adapt<ChangeUserName>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAccount([FromBody] DeleteAccountRequest request, CancellationToken cancellationToken)
    {
        DeleteAccount command = request.Adapt<DeleteAccount>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }
}