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
            poll.Duration.Value
        );

    public static ScoreResponse ToScoreResponse(this Score score)
        => new ScoreResponse(
            score.Gid,
            score.ScoreValue.Value
        );
}
