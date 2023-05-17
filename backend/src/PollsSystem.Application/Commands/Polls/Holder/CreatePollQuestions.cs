using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

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
        RuleFor(x => x.QuestionText)
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

public sealed record CreatePollQuestions(string PollGid, List<QuestionDto> Questions) : ICommand<bool>;

public record QuestionDto(string QuestionText, List<AnswerDto> Answers);

public record AnswerDto(string AnswerText, string ScoreGid);

public class CreatePollQuestionsHandler : BaseCommandHandler<CreatePollQuestions, bool>
{
    private readonly ITransactionalRepository _transactionalRepository;

    public CreatePollQuestionsHandler(
        IUnitOfWork unitOfWork,
        ITransactionalRepository transactionalRepository,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository)
    {
        _transactionalRepository = transactionalRepository ?? throw new ArgumentNullException(nameof(transactionalRepository));
    }

    public override async ValueTask<bool> Handle(CreatePollQuestions command, CancellationToken cancellationToken)
          => await _transactionalRepository.ExecuteTransactionAsync(ProcessQuestionsAndAnswersCreationAsync, command, cancellationToken);

    private async ValueTask<bool> ProcessQuestionsAndAnswersCreationAsync(
       CreatePollQuestions command,
       CancellationToken cancellationToken)
    {
        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == Guid.Parse(command.PollGid));

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {command.PollGid} is null!");

        if (command.Questions.Count < existingPoll.NumberOfQuestions || command.Questions.Count > existingPoll.NumberOfQuestions)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"Number of questions: {command.Questions.Count} in incorrect range");

        foreach (var item in command.Questions)
        {
            bool? isQuestionNameUnqiue = await _baseRepository.IsFieldUniqueAsync<Question>(x => x.QuestionName == item.QuestionText);

            var question = QuestionInit(
                item.QuestionText,
                existingPoll.Gid,
                isQuestionNameUnqiue.GetValueOrDefault()
            );

            _baseRepository.Add<Question>(question);

            foreach (var answerItem in item.Answers)
            {
                bool? isAnswerTextUnique = await _baseRepository.IsFieldUniqueAsync<Answer>(x => x.QuestionGid == question.Gid && x.AnswerText == answerItem.AnswerText);

                var answer = AnswerInit(
                    answerItem.AnswerText,
                    Guid.Parse(answerItem.ScoreGid),
                    question.Gid,
                    isAnswerTextUnique.GetValueOrDefault()
                );

                _baseRepository.Add<Answer>(answer);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
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
