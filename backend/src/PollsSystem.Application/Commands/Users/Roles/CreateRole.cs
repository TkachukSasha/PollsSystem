using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Users.Roles;

public class CreateRoleValidator : AbstractValidator<CreateRole>
{
    public CreateRoleValidator()
    {
        RuleFor(x => x.Name)
            .NotNull();
    }
}

public sealed record CreateRole(string Name) : ICommand<Guid>;

public class CreateRoleHandler : BaseCommandHandler<CreateRole, Guid>
{
    public CreateRoleHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<Guid> Handle(CreateRole command, CancellationToken cancellationToken)
    {
        bool? isRoleNameUnique = await _baseRepository.IsFieldUniqueAsync<Role>(x => x.Name == command.Name);

        var role = Role.Init(
               command.Name,
               isRoleNameUnique.GetValueOrDefault()
        );

        _baseRepository.Add(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Gid;
    }
}
