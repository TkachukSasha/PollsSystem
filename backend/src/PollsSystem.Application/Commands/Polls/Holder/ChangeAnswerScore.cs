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

public sealed record ChangeAnswerScore(string QuestionGid, string AnswerGid, string ScoreGid) : ICommand<Guid>, IValidate
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

public class ChangeAnswerScoreHandler : BaseCommandHandler<ChangeAnswerScore, Guid>
{
    public ChangeAnswerScoreHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<Guid> Handle(ChangeAnswerScore command, CancellationToken cancellationToken)
    {
        var existingQuestion = await _baseRepository.GetByConditionAsync<Question>(x => x.Gid == Guid.Parse(command.QuestionGid));

        if (existingQuestion is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Question with: {command.QuestionGid} is null!");

        var existingAnswer = await _baseRepository.GetByConditionAsync<Answer>(x => x.QuestionGid == Guid.Parse(command.QuestionGid) && x.Gid == Guid.Parse(command.AnswerGid));

        if (existingAnswer is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Answer with: {command.QuestionGid} && {command.AnswerGid} is null!");

        var answer = Answer.ChangeAnswerScore(
               existingAnswer,
               Guid.Parse(command.ScoreGid)
        );

        _baseRepository.Update(answer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return answer.Gid;
    }
}
