using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.ValueObjects.Users;

public sealed class InputName : ValueObject
{
    private const int MinLength = 0;
    private const int MaxLength = 50;

    public string Value { get; }

    private InputName(string value)
    {
        Value = value;
    }

    public static InputName Init(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"FirstName: {value} is null or empty");

        if (value.Length is MinLength or < MinLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"FirstName: {value} is {MinLength} or < {MinLength}");

        if (value.Length > MaxLength)
            throw new BaseException(ExceptionCodes.ValueIsIncorrectRange,
                $"FirstName: {value} is > {MaxLength}");

        return new InputName(value);
    }

    public static implicit operator InputName(string value) => value is null ? null : new InputName(value);
    public static implicit operator string(InputName value) => value.Value;

    public override string ToString() => $"FirstName: {Value}";

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }
}