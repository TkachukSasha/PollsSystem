namespace PollsSystem.Application.Responses.Polls;

public record PollResponse(Guid Gid, string Title, string Description, int NumberOfQuestions, int Duration);

public record ScoreResponse(Guid Gid, double Value);