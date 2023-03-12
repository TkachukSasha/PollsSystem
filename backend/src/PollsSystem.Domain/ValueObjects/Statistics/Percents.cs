using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.ValueObjects.Statistics;

public sealed class Percents : ValueObject
{
    public double Value { get; }

    private Percents(double value)
    {
        Value = value;
    }

    public static Percents Init(double value)
    {
        if (value is < 0.0d || value is > 100.0d)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"Percents: {value} is incorrect range");

        return new Percents(value);
    }

    public static implicit operator Percents(double value) => value is 0 ? null : new Percents(value);
    public static implicit operator double(Percents value) => value.Value;

    public override string ToString() => $"Percents: {Value}";

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }
}