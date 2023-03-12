using PollsSystem.Domain.ValueObjects.Polls;
using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.Entities.Polls;

public class Answer : Entity
{
    private Answer(Text answerText,
                   Guid questionGid,
                   Guid scoreGid) : base()
    {
        AnswerText = answerText;
        QuestionGid = questionGid;
        ScoreGid = scoreGid;
    }

    public Text AnswerText { get; private set; }
    public Guid QuestionGid { get; private set; }
    public Guid ScoreGid { get; private set; }
    public Question? Question { get; set; }
    public Score? Score { get; set; }

    public static Answer Init(
        string answerTextRequest,
        Guid questionGid,
        Guid scoreGid,
        bool isAnswerTextUnique)
    {
        if (!isAnswerTextUnique)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Answer: {answerTextRequest} is already exist");

        var answerText = Text.Init(answerTextRequest);

        var answer = new Answer(answerText,
                                questionGid,
                                scoreGid);

        return answer;
    }

    public static Answer ChangeAnswerText(
        Answer answer,
        string answerTextRequest,
        bool isAnswerTextUnique)
    {
        if (!isAnswerTextUnique)
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Answer: {answerTextRequest} is already exist");

        var answerText = Text.Init(answerTextRequest);

        answer.AnswerText = answerText;

        return answer;
    }

    public static Answer ChangeAnswerScore(
        Answer answer,
        Guid scoreGid)
    {
        answer.ScoreGid = scoreGid;

        return answer;
    }
}
