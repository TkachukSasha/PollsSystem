using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangeQuestionTextValidator : AbstractValidator<ChangeQuestionText>
{
    public ChangeQuestionTextValidator()
    {
        RuleFor(x => x.QuestionGid)
            .NotNull();

        RuleFor(x => x.Question)
            .MinimumLength(6)
            .MaximumLength(50)
            .NotNull();
    }
}

public sealed record ChangeQuestionText(string QuestionGid, string Question) : ICommand<bool>;

public class ChangeQuestionTextHandler : BaseCommandHandler<ChangeQuestionText, bool>
{
    public ChangeQuestionTextHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(ChangeQuestionText command, CancellationToken cancellationToken)
    {
        var existingQuestion = await _baseRepository.GetByConditionAsync<Question>(x => x.Gid == Guid.Parse(command.QuestionGid));

        if (existingQuestion is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Question with: {command.QuestionGid} is null!");

        if (existingQuestion.QuestionName == command.Question)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Answer with: {command.Question} is already exist!");

        bool? isQuestionNameUnique = await _baseRepository.IsFieldUniqueAsync<Question>(x => x.QuestionName == command.Question);

        var question = Question.ChangeQuestionName(
            existingQuestion,
            command.Question,
            isQuestionNameUnique.GetValueOrDefault()
        );

        _baseRepository.Update(question);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return question.Gid == Guid.Empty ? false : true;
    }
}
