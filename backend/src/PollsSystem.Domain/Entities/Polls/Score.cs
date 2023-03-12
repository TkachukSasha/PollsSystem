using PollsSystem.Domain.ValueObjects.Polls;
using PollsSystem.Shared.Abstractions.Primitives;

namespace PollsSystem.Domain.Entities.Polls;

public class Score : Entity
{
    private Score(ScoreValue scoreValue) : base()
    {
        ScoreValue = scoreValue;
    }

    public ScoreValue ScoreValue { get; private set; }

    public static Score Init(double scoreValueRequest)
    {
        var scoreValue = ScoreValue.Init(scoreValueRequest);

        var score = new Score(scoreValue);

        return score;
    }
}
