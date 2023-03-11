namespace PollsSystem.Shared.Abstractions.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetValues();

    public override bool Equals(object? obj)
        => obj is ValueObject other && ValuesAreEqual(other);

    public bool Equals(ValueObject? other)
        => other is not null && ValuesAreEqual(other);

    public override int GetHashCode()
        => GetValues().Aggregate(default(int), HashCode.Combine);

    private bool ValuesAreEqual(ValueObject other)
        => GetValues().SequenceEqual(other.GetValues());
}