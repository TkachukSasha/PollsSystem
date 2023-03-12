using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.ValueObjects.Polls;

public sealed class ScoreValue : ValueObject
{
    private const double MaxLength = 240.0d;

    public double Value { get; }

    private ScoreValue(double value)
    {
        Value = value;
    }

    public static ScoreValue Init(double value)
    {
        if (value > MaxLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"ScoreValue: {value} is incorrect range");

        return new ScoreValue(value);
    }

    public static implicit operator ScoreValue(double value) => value is 0 ? null : new ScoreValue(value);
    public static implicit operator double(ScoreValue value) => value.Value;

    public override string ToString() => $"ScoreValue: {Value}";

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }
}