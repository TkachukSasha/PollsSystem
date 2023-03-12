using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.ValueObjects.Polls;

public sealed class Key : ValueObject
{
    public string Value { get; }

    private Key(string value)
    {
        Value = value;
    }

    public static Key Init(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Key: {value} is null or empty");

        return new Key(value);
    }

    public static implicit operator Key(string value) => value is null ? null : new Key(value);
    public static implicit operator string(Key value) => value.Value;

    public override string ToString() => $"Key: {Value}";

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }
}