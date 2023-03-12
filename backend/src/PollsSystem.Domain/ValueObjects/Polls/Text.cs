using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.ValueObjects.Polls;

public sealed class Text : ValueObject
{
    private const int MinLength = 0;
    private const int MaxLength = 250;

    public string Value { get; }

    private Text(string value)
    {
        Value = value;
    }

    public static Text Init(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"AnswerText: {value} is null or empty");

        if (value.Length is MinLength or < MinLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"AnswerText: {value} is incorrect range");

        if (value.Length > MaxLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"AnswerText: {value} is incorrect range");

        return new Text(value);
    }

    public static implicit operator Text(string value) => value is null ? null : new Text(value);
    public static implicit operator string(Text value) => value.Value;

    public override string ToString() => $"Text: {Value}";

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }
}