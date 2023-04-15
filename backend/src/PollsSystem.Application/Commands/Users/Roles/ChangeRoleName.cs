using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Users.Roles;

public class ChangeRoleNameValidator : AbstractValidator<ChangeRoleName>
{
    public ChangeRoleNameValidator()
    {
        RuleFor(x => x.CurrentName)
            .NotNull();

        RuleFor(x => x.Name)
            .NotNull();
    }
}

public sealed record ChangeRoleName(string CurrentName, string Name) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangeRoleNameValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangeRoleNameHandler : BaseCommandHandler<ChangeRoleName, Guid>
{
    public ChangeRoleNameHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<Guid> Handle(ChangeRoleName command, CancellationToken cancellationToken)
    {
        bool? isRoleNameUnique = await _baseRepository.IsFieldUniqueAsync<Role>(x => x.Name == command.Name);

        var existingRole = await _baseRepository.GetByConditionAsync<Role>(x => x.Name == command.CurrentName);

        if (existingRole is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Role with: {command.CurrentName} is null!");

        if (existingRole.Name == command.Name)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Role with: {command.Name} is already exist!");

        var role = Role.ChangeRoleName(
                existingRole,
                command.Name,
                isRoleNameUnique.GetValueOrDefault()
        );

        _baseRepository.Update(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Gid;
    }
}
