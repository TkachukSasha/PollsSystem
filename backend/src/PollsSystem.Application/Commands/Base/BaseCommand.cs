using Mediator;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Base;

public abstract class BaseCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IBaseRepository _baseRepository;

    public BaseCommandHandler(
       IUnitOfWork unitOfWork,
       IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public abstract ValueTask<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}