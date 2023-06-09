﻿using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Users.Roles;

public class DeleteRoleValidator : AbstractValidator<DeleteRole>
{
    public DeleteRoleValidator()
    {
        RuleFor(x => x.RoleGid)
            .NotNull();
    }
}

public sealed record DeleteRole(Guid RoleGid) : ICommand<Guid>;

public class DeleteRoleHandler : BaseCommandHandler<DeleteRole, Guid>
{
    public DeleteRoleHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<Guid> Handle(DeleteRole command, CancellationToken cancellationToken)
    {
        var existingRole = await _baseRepository.GetByConditionAsync<Role>(x => x.Gid == command.RoleGid);

        if (existingRole is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Role with: {command.RoleGid} is null!");

        // TODO: Delete On Cascade or etc
        var roleGid = _baseRepository.Delete<Role>(existingRole.Gid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return roleGid;
    }
}
