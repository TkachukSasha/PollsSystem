using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.ValueObjects.Polls;
using PollsSystem.Domain.ValueObjects.Statistics;
using PollsSystem.Domain.ValueObjects.Users;
using PollsSystem.Shared.Abstractions.Primitives;

namespace PollsSystem.Domain.Entities.Statistics;

public sealed class Result : Entity
{
    private Result(
        ScoreValue score,
        Percents percents,
        InputName firstName,
        InputName lastName,
        Guid pollGid) : base()
    {
        Score = score;
        Percents = percents;
        FirstName = firstName;
        LastName = lastName;
        PollGid = pollGid;
    }

    public ScoreValue Score { get; private set; }
    public Percents Percents { get; private set; }
    public InputName FirstName { get; private set; }
    public InputName LastName { get; private set; }
    public Guid PollGid { get; private set; }
    public Poll Poll { get; private set; }

    public static Result Init(
        double scoreRequest,
        double percentsRequest,
        string firstNameRequest,
        string lastNameRequest,
        Guid pollGid)
    {
        var score = ScoreValue.Init(scoreRequest);
        var percents = Percents.Init(percentsRequest);
        var firstName = InputName.Init(firstNameRequest);
        var lastName = InputName.Init(lastNameRequest);

        var result = new Result(
           score,
           percents,
           firstName,
           lastName,
           pollGid
        );

        return result;
    }

    public static Result ChangeScore(
        Result result,
        double scoreRequest)
    {
        var score = ScoreValue.Init(scoreRequest);

        result.Score = score;

        return result;
    }
}