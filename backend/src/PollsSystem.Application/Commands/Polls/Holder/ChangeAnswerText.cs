using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangeAnswerTextValidator : AbstractValidator<ChangeAnswerText>
{
    public ChangeAnswerTextValidator()
    {
        RuleFor(x => x.QuestionGid)
            .NotNull();

        RuleFor(x => x.AnswerGid)
            .NotNull();

        RuleFor(x => x.Text)
            .MinimumLength(8)
            .MinimumLength(16)
            .NotNull();
    }
}

public sealed record ChangeAnswerText(Guid QuestionGid, Guid AnswerGid, string Text) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new ChangeAnswerTextValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class ChangeAnswerTextHandler : ICommandHandler<ChangeAnswerText, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository _baseRepository;

    public ChangeAnswerTextHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }

    public async ValueTask<Guid> Handle(ChangeAnswerText command, CancellationToken cancellationToken)
    {
        var existingQuestion = await _baseRepository.GetByConditionAsync<Question>(x => x.Gid == command.QuestionGid);

        if (existingQuestion is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Question with: {command.QuestionGid} is null!");

        var existingAnswer = await _baseRepository.GetByConditionAsync<Answer>(x => x.QuestionGid == command.QuestionGid && x.Gid == command.AnswerGid);

        if (existingAnswer is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Answer with: {command.QuestionGid} && {command.AnswerGid} is null!");

        if (existingAnswer.AnswerText == command.Text)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Answer with: {command.Text} is already exist!");

        bool? isAnswerTextUnique = await _baseRepository.IsFieldUniqueAsync<Answer>(x => x.QuestionGid == command.QuestionGid && x.AnswerText == command.Text);

        var answer = Answer.ChangeAnswerText(
                existingAnswer,
                command.Text,
                isAnswerTextUnique.GetValueOrDefault()
        );

        _baseRepository.Update(answer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return answer.Gid;
    }
}
