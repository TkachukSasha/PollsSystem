using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
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

public sealed record DeletePoll(string PollGid) : ICommand<bool>, IValidate
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

public class DeletePollHandler : BaseCommandHandler<DeletePoll, bool>
{
    private readonly ITransactionalRepository _transactionalRepository;

    public DeletePollHandler(
        IUnitOfWork unitOfWork,
        ITransactionalRepository transactionalRepository,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository)
    {
        _transactionalRepository = transactionalRepository ?? throw new ArgumentNullException(nameof(transactionalRepository));
    }

    public override async ValueTask<bool> Handle(DeletePoll command, CancellationToken cancellationToken)
        => await _transactionalRepository.ExecuteTransactionAsync(ProcessPollDelete, command, cancellationToken);

    private async ValueTask<bool> ProcessPollDelete(
        DeletePoll command,
        CancellationToken cancellationToken)
    {
        List<Answer> answers = new();

        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == Guid.Parse(command.PollGid));

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        try
        {
            var questions = await _baseRepository.GetEntitiesByConditionAsync<Question>(x => x.PollGid == Guid.Parse(command.PollGid));

            return questions.Any() ? await DeleteAllRangeAsync(existingPoll, questions, cancellationToken)
                                   : await DeletePollAsync(existingPoll, cancellationToken);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private async ValueTask<bool> DeleteAllRangeAsync(
        Poll poll,
        IEnumerable<Question> questions,
        CancellationToken cancellationToken)
    {
        List<Answer> answers = new();

        foreach (var question in questions)
        {
            var answersCollection = await _baseRepository.GetEntitiesByConditionAsync<Answer>(x => x.QuestionGid == question.Gid);

            answers = answersCollection.ToList();
        }

        _baseRepository.DeleteRange(answers);

        _baseRepository.DeleteRange(questions);

        _baseRepository.Delete<Poll>(poll.Gid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async ValueTask<bool> DeletePollAsync(
        Poll poll,
        CancellationToken cancellationToken)
    {
        _baseRepository.Delete<Poll>(poll.Gid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
