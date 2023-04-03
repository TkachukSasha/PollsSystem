namespace PollsSystem.Presentation.Statistics.Results.Requests;

public record ChangeResultScoreRequest(string PollGid, string LastName, double Score);

public record DeleteResultRequest(string PollGid, string ResultGid);

public record GetResultsQuery(string PollGid);

public record GetResultQuery(string PollGid, string LastName);

public record GetResultsByLastNameQuery(string PollGid, string LastName);