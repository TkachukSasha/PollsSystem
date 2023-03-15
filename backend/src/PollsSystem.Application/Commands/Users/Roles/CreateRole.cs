using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Users.Roles;

public class CreateRoleValidator : AbstractValidator<CreateRole>
{
    public CreateRoleValidator()
    {
        RuleFor(x => x.Name)
            .NotNull();
    }
}

public sealed record CreateRole(string Name) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new CreateRoleValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class CreateRoleHandler : ICommandHandler<CreateRole, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;

    public CreateRoleHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(CreateRole command, CancellationToken cancellationToken)
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
