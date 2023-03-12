using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.ValueObjects.Polls;

public sealed class NumberOfQuestions : ValueObject
{
    private const int MinLength = 0;
    private const int MaxLength = 100;

    public int Value { get; }

    private NumberOfQuestions(int value)
    {
        Value = value;
    }

    public static NumberOfQuestions Init(int value)
    {
        if (value is MinLength or < MinLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"NumberOfQuestions: {value} is incorrect range");

        if (value > MaxLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"NumberOfQuestions: {value} is incorrect range");

        return new NumberOfQuestions(value);
    }

    public static implicit operator NumberOfQuestions(int value) => value is 0 ? null : new NumberOfQuestions(value);
    public static implicit operator int(NumberOfQuestions value) => value.Value;

    public override string ToString() => $"NumberOfQuestions: {Value}";

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }
}