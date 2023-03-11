namespace PollsSystem.Shared.Abstractions.Primitives;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? LastModified { get; set; }
}