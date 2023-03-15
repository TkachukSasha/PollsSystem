using Mapster;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollsSystem.Application.Commands.Users.Roles;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Presentation.Users.Roles;
using PollsSystem.Presentation.Users.Roles.Requests;
using PollsSystem.Shared.Dal.Repositories;

namespace PollsSystem.Api.Controllers.Users;

[Authorize(Policy = "admin")]
[Route("api/roles")]
public class RolesController : BaseController
{
    private readonly IBaseRepository _repository;

    public RolesController(
        IMediator mediator,
        IBaseRepository repository)
        : base(mediator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetRoles()
    {
        var roles = await _repository.GetListAsync<Role>();

        return roles is null ? NoContent() : Ok(roles.Select(x => x?.ToRoleResponse()).ToList());
    }

    [HttpGet("by-name")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> GetRoleByName(GetRoleByNameQuery query)
    {
        var role = await _repository.GetByConditionAsync<Role>(x => x.Name == query.Name);

        return role is null ? NoContent() : Ok(role.ToRoleResponse());
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        CreateRole command = request.Adapt<CreateRole>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-name")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> ChangeName(ChangeRoleNameRequest request, CancellationToken cancellationToken)
    {
        ChangeRoleName command = request.Adapt<ChangeRoleName>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("change-name")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteRole(DeleteRoleRequest request, CancellationToken cancellationToken)
    {
        DeleteRole command = request.Adapt<DeleteRole>();

        return Ok(await _mediator.Send(command, cancellationToken));
    }
}