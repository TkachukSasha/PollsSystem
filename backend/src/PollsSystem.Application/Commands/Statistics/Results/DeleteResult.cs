using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Statistics.Results;

public class DeleteResultValidator : AbstractValidator<DeleteResult>
{
    public DeleteResultValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.ResultGid)
            .NotNull();
    }
}

public sealed record DeleteResult(string PollGid, string ResultGid) : ICommand<bool>;

public class DeleteResultHandler : BaseCommandHandler<DeleteResult, bool>
{
    public DeleteResultHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(DeleteResult command, CancellationToken cancellationToken)
    {
        var existingResult = await _baseRepository.GetByConditionAsync<Result>(x => x.Gid == Guid.Parse(command.ResultGid) && x.PollGid == Guid.Parse(command.PollGid));

        if (existingResult is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Result with: {command.ResultGid} is null!");

        var resultGid = _baseRepository.Delete<Result>(existingResult.Gid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return resultGid != Guid.Empty ? true : false;
    }
}
