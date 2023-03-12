namespace PollsSystem.Application.Commands.Validation;

public sealed record ValidationError(IEnumerable<string> Errors);