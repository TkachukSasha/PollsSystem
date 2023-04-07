using Mapster;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Commands.Users.Accounts;
using PollsSystem.Application.Responses.Users;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Presentation.Users.Accounts.Requests;
using PollsSystem.Presentation.Users.UsersManagement.Requests;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Security.Storage;

namespace PollsSystem.Api.Controllers.Users.v1;

[Authorize]
[Route("api/v{version:apiVersion}/accounts")]
[ApiVersion("1.0")]
public class AccountsController : BaseController
{
    private readonly IStorage _storage;
    private readonly IBaseRepository _repository;

    public AccountsController(
        IMediator mediator,
        IStorage storage,
        IBaseRepository repository)
        : base(mediator)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

    [HttpGet("username")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetUserName([FromQuery] GetUserQuery query)
    {
        var userGid = Guid.Parse(query.UserGid);

        var user = await _repository.GetByConditionAsync<User>(x => x.Gid == userGid);

        return user is not null ? Ok(new { userName = user.UserName.Value }) : NoContent();
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