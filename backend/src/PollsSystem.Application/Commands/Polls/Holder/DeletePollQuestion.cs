using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class DeletePollQuestionValidator : AbstractValidator<DeletePollQuestion>
{
    public DeletePollQuestionValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleFor(x => x.QuestionGid)
            .NotNull();
    }
}

public sealed record DeletePollQuestion(string PollGid, string QuestionGid) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new DeletePollQuestionValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class DeletePollQuestionHandler : ICommandHandler<DeletePollQuestion, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalRepository _transactionalRepository;
    private readonly IBaseRepository _baseRepository;

    public DeletePollQuestionHandler(
        IUnitOfWork unitOfWork,
        ITransactionalRepository transactionalRepository,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _transactionalRepository = transactionalRepository ?? throw new ArgumentNullException(nameof(transactionalRepository));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(DeletePollQuestion command, CancellationToken cancellationToken)
        => await _transactionalRepository.ExecuteTransactionAsync(ProcessQuestionDelete, command, cancellationToken);

    private async ValueTask<Guid> ProcessQuestionDelete(
        DeletePollQuestion command,
        CancellationToken cancellationToken)
    {
        var existingQuestion = await _baseRepository.GetByConditionAsync<Question>(x => x.PollGid == Guid.Parse(command.PollGid) && x.Gid == Guid.Parse(command.QuestionGid));

        if (existingQuestion is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Quetion with: {command.PollGid} && {command.QuestionGid} is null!");

        var questionGid = _baseRepository.Delete<Question>(existingQuestion.Gid);

        _baseRepository.DeleteByCondition<Answer>(x => x.QuestionGid == questionGid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return questionGid;
    }
}
