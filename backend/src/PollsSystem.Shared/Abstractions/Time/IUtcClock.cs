namespace PollsSystem.Shared.Abstractions.Time;

public interface IUtcClock
{
    DateTime GetCurrentUtc();
}

public class UtcClock : IUtcClock
{
    public DateTime GetCurrentUtc() => DateTime.UtcNow;
}