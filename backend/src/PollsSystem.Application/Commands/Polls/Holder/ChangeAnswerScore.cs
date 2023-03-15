using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangeAnswerScoreValidator : AbstractValidator<ChangeAnswerScore>
{
    public ChangeAnswerScoreValidator()
    {
        RuleFor(x => x.QuestionGid)
            .NotNull();

        RuleFor(x => x.AnswerGid)
            .NotNull();

        RuleFor(x => x.ScoreGid)
           .NotNull();
    }
}

public sealed record ChangeAnswerScore(Guid QuestionGid, Guid AnswerGid, Guid ScoreGid) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangeAnswerScoreValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangeAnswerScoreHandler : ICommandHandler<ChangeAnswerScore, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;

    public ChangeAnswerScoreHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(ChangeAnswerScore command, CancellationToken cancellationToken)
    {
        var existingQuestion = await _baseRepository.GetByConditionAsync<Question>(x => x.Gid == command.QuestionGid);

        if (existingQuestion is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Question with: {command.QuestionGid} is null!");

        var existingAnswer = await _baseRepository.GetByConditionAsync<Answer>(x => x.QuestionGid == command.QuestionGid && x.Gid == command.AnswerGid);

        if (existingAnswer is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Answer with: {command.QuestionGid} && {command.AnswerGid} is null!");

        var answer = Answer.ChangeAnswerScore(
               existingAnswer,
               command.ScoreGid
        );

        _baseRepository.Update(answer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return answer.Gid;
    }
}
