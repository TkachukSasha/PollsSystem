using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Users.Accounts;

public class ChangeUserNameValidator : AbstractValidator<ChangeUserName>
{
    public ChangeUserNameValidator()
    {
        RuleFor(x => x.CurrentUserName)
            .NotNull();

        RuleFor(x => x.UserName)
            .MinimumLength(8)
            .MaximumLength(16)
            .NotNull();
    }
}

public sealed record ChangeUserName(string CurrentUserName, string UserName) : ICommand<bool>;

public class ChangeUserNameHandler : BaseCommandHandler<ChangeUserName, bool>
{
    public ChangeUserNameHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(ChangeUserName command, CancellationToken cancellationToken)
    {
        var existingUser = await _baseRepository.GetByConditionAsync<User>(x => x.UserName == command.CurrentUserName);

        if (existingUser is null)
            throw new BaseException(ExceptionCodes.ValueMissmatch,
                $"User with: {command.CurrentUserName} is not found!");

        bool? isUserNameUnique = await _baseRepository.IsFieldUniqueAsync<User>(x => x.UserName == command.UserName);

        var user = User.ChangeUserName(
            existingUser,
            command.UserName,
            isUserNameUnique.GetValueOrDefault());

        _baseRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Gid != Guid.Empty ? true : false;
    }
}
