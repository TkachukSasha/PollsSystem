using PollsSystem.Domain.ValueObjects.Polls;
using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.Entities.Polls;

public class Question : Entity
{
    private readonly List<Answer> _answers = new();

    private Question(Text questionName,
                     Guid pollGid) : base()
    {
        QuestionName = questionName;
        PollGid = pollGid;
    }

    public Text QuestionName { get; private set; }
    public Guid PollGid { get; private set; }
    public Poll? Poll { get; set; }

    public ICollection<Answer> Answers => _answers;

    public static Question Init(
        string questionNameRequest,
        Guid pollGid,
        bool isQuestionNameUnqiue)
    {
        if (!isQuestionNameUnqiue)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Question with: {questionNameRequest} is already exist");

        var questionName = Text.Init(questionNameRequest);

        var question = new Question(questionName,
                                    pollGid);

        return question;
    }

    public static Question ChangeQuestionName(Question question,
                                              string questionNameRequest,
                                              bool isQuestionNameUnqiue)
    {
        if (!isQuestionNameUnqiue)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Question with: {questionNameRequest} is already exist");

        var questionName = Text.Init(questionNameRequest);

        question.QuestionName = questionName;

        return question;
    }
}