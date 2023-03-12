using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.ValueObjects.Polls;

public sealed class Duration : ValueObject
{
    private const int MinLength = 0;
    private const int MaxLength = 240;

    public int Value { get; }

    private Duration(int value)
    {
        Value = value;
    }

    public static Duration Init(int value)
    {
        if (value is MinLength or < MinLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"Value: {value} is incorrect range");

        if (value > MaxLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"Value: {value} is incorrect range");

        return new Duration(value);
    }

    public static implicit operator Duration(int value) => value is 0 ? null : new Duration(value);
    public static implicit operator int(Duration value) => value.Value;

    public override string ToString() => $"Duration: {Value}";

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }
}