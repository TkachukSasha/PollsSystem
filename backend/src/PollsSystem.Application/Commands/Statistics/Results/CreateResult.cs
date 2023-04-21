using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Statistics.Results;

public class CreateResultValidator : AbstractValidator<CreateResult>
{
    public CreateResultValidator()
    {
        RuleFor(x => x.Score)
            .NotNull();

        RuleFor(x => x.Percents)
            .NotNull();

        RuleFor(x => x.FirstName)
            .NotNull();

        RuleFor(x => x.LastName)
            .NotNull();

        RuleFor(x => x.PollGid)
            .NotNull();
    }
}

public sealed record CreateResult(double Score, double Percents, string FirstName, string LastName, string PollGid) : ICommand<Guid>;

public class CreateResultHandler : BaseCommandHandler<CreateResult, Guid>
{
    public CreateResultHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<Guid> Handle(CreateResult command, CancellationToken cancellationToken)
    {
        var existingResult = await _baseRepository.GetByConditionAsync<Result>(x => x.PollGid == Guid.Parse(command.PollGid) && x.LastName == command.LastName);

        if (existingResult is not null)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Result by: {command.LastName} already stored");

        var result = Result.Init(
               command.Score,
               command.Percents,
               command.FirstName,
               command.LastName,
               Guid.Parse(command.PollGid)
          );

        _baseRepository.Add(result);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Gid;
    }
}
