using PollsSystem.Application.Responses.Polls;
using PollsSystem.Domain.Entities.Polls;

namespace PollsSystem.Presentation.Polls.PollsManagement;

public static class PollsQueryMapper
{
    public static PollResponse ToPollResponse(this Poll poll)
        => new PollResponse(
            poll.Gid,
            poll.Title.Value,
            poll.Description.Value,
            poll.NumberOfQuestions.Value,
            poll.Duration.Value,
            poll.Key.Value
        );

    public static QuestionsWithAnswersDefaultResponse ToQuestionsWithAnswersResponse(this Question question, List<Answer> answers)
        => new QuestionsWithAnswersDefaultResponse(
            question.Gid,
            question.QuestionName.Value,
            answers.Select(x => x.ToAnswerResponse()).ToList()
        );

    public static AnswerDefaultResponse ToAnswerResponse(this Answer answer)
        => new AnswerDefaultResponse(
            answer.Gid,
            answer.AnswerText.Value
        );

    public static QuestionsWithAnswersAndScoresResponse ToQuestionsWithAnswersWithScoresResponse(this Question question, List<Answer> answers)
        => new QuestionsWithAnswersAndScoresResponse(
            question.Gid,
            question.QuestionName.Value,
            answers.Select(x => x.ToAnswerWithScoresResponse()).ToList()
        );

    public static AnswerWithScoresResponse ToAnswerWithScoresResponse(this Answer answer)
        => new AnswerWithScoresResponse(
            answer.Gid,
            answer.AnswerText.Value,
            answer.ScoreGid.ToString()
        );

    public static ScoreResponse ToScoreResponse(this Score score)
        => new ScoreResponse(
            score.Gid,
            score.ScoreValue.Value
        );
}
