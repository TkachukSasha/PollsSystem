using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

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

public sealed record DeletePollQuestion(string PollGid, string QuestionGid) : ICommand<bool>;

public class DeletePollQuestionHandler : BaseCommandHandler<DeletePollQuestion, bool>
{
    private readonly ITransactionalRepository _transactionalRepository;

    public DeletePollQuestionHandler(
        IUnitOfWork unitOfWork,
        ITransactionalRepository transactionalRepository,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository)
    {
        _transactionalRepository = transactionalRepository ?? throw new ArgumentNullException(nameof(transactionalRepository));
    }

    public override async ValueTask<bool> Handle(DeletePollQuestion command, CancellationToken cancellationToken)
        => await _transactionalRepository.ExecuteTransactionAsync(ProcessQuestionDelete, command, cancellationToken);

    private async ValueTask<bool> ProcessQuestionDelete(
        DeletePollQuestion command,
        CancellationToken cancellationToken)
    {
        var existingQuestion = await _baseRepository.GetByConditionAsync<Question>(x => x.PollGid == Guid.Parse(command.PollGid) && x.Gid == Guid.Parse(command.QuestionGid));

        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == existingQuestion.PollGid);

        if (existingQuestion is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Quetion with: {command.PollGid} && {command.QuestionGid} is null!");

        var questionGid = _baseRepository.Delete<Question>(existingQuestion.Gid);

        var poll = Poll.ChangePollNumberOfQuestions(
            existingPoll
        );

        _baseRepository.Update(poll);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return questionGid == Guid.Empty ? false : true;
    }
}
