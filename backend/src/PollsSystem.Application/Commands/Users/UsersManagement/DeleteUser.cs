using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Users.UsersManagement;

public class DeleteUserValidator : AbstractValidator<DeleteUser>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.UserGid)
            .NotNull();
    }
}

public sealed record DeleteUser(Guid UserGid) : ICommand<Guid>;

public class DeleteUserHandler : BaseCommandHandler<DeleteUser, Guid>
{
    public DeleteUserHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<Guid> Handle(DeleteUser command, CancellationToken cancellationToken)
    {
        var existingUser = await _baseRepository.GetByConditionAsync<User>(x => x.Gid == command.UserGid);

        if (existingUser is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"User with: {command.UserGid} is null!");

        var userGid = _baseRepository.Delete<User>(existingUser.Gid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return userGid;
    }
}
