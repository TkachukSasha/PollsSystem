using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class CreatePollQuestionsValidator : AbstractValidator<CreatePollQuestions>
{
    public CreatePollQuestionsValidator()
    {
        RuleFor(x => x.PollGid)
            .NotNull();

        RuleForEach(x => x.Questions).SetValidator(new QuestionDtoValidator());
    }
}

public class QuestionDtoValidator : AbstractValidator<QuestionDto>
{
    public QuestionDtoValidator()
    {
        RuleFor(x => x.QuestionName)
            .MinimumLength(6)
            .MaximumLength(124)
            .NotNull();

        RuleForEach(x => x.Answers).SetValidator(new AnswerDtoValidator());
    }
}

public class AnswerDtoValidator : AbstractValidator<AnswerDto>
{
    public AnswerDtoValidator()
    {
        RuleFor(x => x.AnswerText)
            .MinimumLength(6)
            .MaximumLength(124)
            .NotNull();

        RuleFor(x => x.ScoreGid)
            .NotNull();
    }
}

public sealed record CreatePollQuestions(Guid PollGid, List<QuestionDto> Questions) : ICommand<Guid>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new CreatePollQuestionsValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public record QuestionDto(string QuestionName, List<AnswerDto> Answers);

public record AnswerDto(string AnswerText, Guid ScoreGid);

public class CreatePollQuestionsHandler : ICommandHandler<CreatePollQuestions, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalRepository _transactionalRepository;
    private readonly IBaseRepository _baseRepository;

    public CreatePollQuestionsHandler(
        IUnitOfWork unitOfWork,
        ITransactionalRepository transactionalRepository,
        IBaseRepository baseRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _transactionalRepository = transactionalRepository ?? throw new ArgumentNullException(nameof(transactionalRepository));
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
    }


    public async ValueTask<Guid> Handle(CreatePollQuestions command, CancellationToken cancellationToken)
          => await _transactionalRepository.ExecuteTransactionAsync(ProcessQuestionsAndAnswersCreation, command, cancellationToken);

    private async ValueTask<Guid> ProcessQuestionsAndAnswersCreation(
       CreatePollQuestions command,
       CancellationToken cancellationToken)
    {
        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == command.PollGid);

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        if (command.Questions.Count < existingPoll.NumberOfQuestions || command.Questions.Count > existingPoll.NumberOfQuestions)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"Number of questions: {command.Questions.Count} in incorrect range");

        foreach (var item in command.Questions)
        {
            bool? isQuestionNameUnqiue = await _baseRepository.IsFieldUniqueAsync<Question>(x => x.QuestionName == item.QuestionName);

            var question = QuestionInit(
                item.QuestionName,
                existingPoll.Gid,
                isQuestionNameUnqiue.GetValueOrDefault()
            );

            _baseRepository.Add<Question>(question);

            foreach (var answerItem in item.Answers)
            {
                bool? isAnswerTextUnique = await _baseRepository.IsFieldUniqueAsync<Answer>(x => x.QuestionGid == question.Gid && x.AnswerText == answerItem.AnswerText);

                var answer = AnswerInit(
                    answerItem.AnswerText,
                    answerItem.ScoreGid,
                    question.Gid,
                    isAnswerTextUnique.GetValueOrDefault()
                );

                _baseRepository.Add<Answer>(answer);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return existingPoll.Gid;
    }


    private Question QuestionInit(string questionName, Guid pollGid, bool isQuestionNameUnqiue)
    {
        var question = Question.Init(
                   questionName,
                   pollGid,
                   isQuestionNameUnqiue
        );

        return question;
    }

    private Answer AnswerInit(string answerText, Guid scoreGid, Guid questionGid, bool isAnswerTextUnique)
    {
        var answer = Answer.Init(
                answerText,
                questionGid,
                scoreGid,
                isAnswerTextUnique
        );

        return answer;
    }
}
