using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

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

public sealed record CreateResult(double Score, double Percents, string FirstName, string LastName, string PollGid) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new CreateResultValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class CreateResultHandler : ICommandHandler<CreateResult, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;

    public CreateResultHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(CreateResult command, CancellationToken cancellationToken)
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
