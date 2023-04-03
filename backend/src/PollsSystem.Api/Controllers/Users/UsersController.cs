using Mapster;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Commands.Users.UsersManagement;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Presentation.Users.UsersManagement;
using PollsSystem.Presentation.Users.UsersManagement.Requests;
using PollsSystem.Shared.Dal.Repositories;

namespace PollsSystem.Api.Controllers.Users;

[Authorize(Policy = "admin")]
[Route("api/users")]
public class UsersController : BaseController
{
    private readonly IBaseRepository _repository;

    public UsersController(
        IMediator mediator,
        IBaseRepository repository)
        : base(mediator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetUsers()
    {
        var users = await _repository.GetListAsync<User>();

        var response = users?.Select(x => x?.ToUserResponse());

        return response is null ? NoContent() : Ok(users);
    }

    [HttpGet("user")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetUser([FromQuery] GetUserQuery query)
    {
        var userGid = Guid.Parse(query.UserGid);

        var user = await _repository.GetByConditionAsync<User>(x => x.Gid == userGid);

        var userRole = await _repository.GetByConditionAsync<Role>(x => x.Gid == user.RoleGid);

        return Ok(user.ToUserExtendedResponse(userRole.Name));
    }

    [HttpGet("by-input")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetUserByInputName([FromQuery] GetUserByInputNameQuery query)
    {
        var user = await _repository.GetByConditionAsync<User>(x => x.LastName == query.InputName);

        var userRole = await _repository.GetByConditionAsync<Role>(x => x.Gid == user.RoleGid);

        return Ok(user.ToUserExtendedResponse(userRole.Name));
    }

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteUser([FromBody] DeleteUserRequest request, CancellationToken cancellationToken)
    {
        DeleteUser command = request.Adapt<DeleteUser>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }
}