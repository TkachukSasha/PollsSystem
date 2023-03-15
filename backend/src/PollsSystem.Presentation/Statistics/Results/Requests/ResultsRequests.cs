namespace PollsSystem.Presentation.Statistics.Results.Requests;

public record ChangeResultScoreRequest(Guid PollGid, string LastName, double Score);

public record DeleteResultRequest(Guid PollGid, Guid ResultGid);

public record GetResultsQuery(Guid PollGid, PaginationData Data);

public record GetResultsByLastNameQuery(Guid PollGid, string LastName, PaginationData Data);