using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class DeletePollValidator : AbstractValidator<DeletePoll>
{
    public DeletePollValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();
    }
}

public sealed record DeletePoll(string PollGid) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new DeletePollValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class DeletePollHandler : ICommandHandler<DeletePoll, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalRepository _transactionalRepository;
    private readonly IBaseRepository _baseRepository;

    public DeletePollHandler(
        IUnitOfWork unitOfWork,
        ITransactionalRepository transactionalRepository,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _transactionalRepository = transactionalRepository ?? throw new ArgumentNullException(nameof(transactionalRepository));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(DeletePoll command, CancellationToken cancellationToken)
        => await _transactionalRepository.ExecuteTransactionAsync(ProcessPollDelete, command, cancellationToken);

    private async ValueTask<Guid> ProcessPollDelete(
        DeletePoll command,
        CancellationToken cancellationToken)
    {
        List<Answer> answers = new();

        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == Guid.Parse(command.PollGid));

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        var questions = await _baseRepository.GetEntitiesByConditionAsync<Question>(x => x.PollGid == Guid.Parse(command.PollGid));

        if (questions.Any())
        {
            foreach (var question in questions)
            {
                var answersCollection = await _baseRepository.GetEntitiesByConditionAsync<Answer>(x => x.QuestionGid == question.Gid);

                answers = answersCollection.ToList();
            }

            _baseRepository.DeleteRange<Answer>(answers);

            _baseRepository.DeleteRange<Question>(questions);

            _baseRepository.Delete<Poll>(existingPoll.Gid);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return existingPoll.Gid;
    }
}
