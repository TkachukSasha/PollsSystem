using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Statistics.Results;

public class ChangeResultScoreValidator : AbstractValidator<ChangeResultScore>
{
    public ChangeResultScoreValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.LastName)
            .NotNull();

        RuleFor(x => x.Score)
           .NotNull();
    }
}

public sealed record ChangeResultScore(string PollGid, string LastName, double Score) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangeResultScoreValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangeResultScoreHandler : BaseCommandHandler<ChangeResultScore, Guid>
{
    public ChangeResultScoreHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<Guid> Handle(ChangeResultScore command, CancellationToken cancellationToken)
    {
        var existingResult = await _baseRepository.GetByConditionAsync<Result>(x => x.PollGid == Guid.Parse(command.PollGid) && x.LastName == command.LastName);

        if (existingResult is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Result by: {command.LastName} is null!");

        if (existingResult.Score == command.Score)
            throw new BaseException(ExceptionCodes.ValueMissmatch,
                $"Score: {command.Score} missmatch!");

        var result = Result.ChangeScore(
              existingResult,
              command.Score
          );

        _baseRepository.Update(result);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Gid;
    }
}
