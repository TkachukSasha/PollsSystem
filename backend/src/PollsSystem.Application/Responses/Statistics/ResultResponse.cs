namespace PollsSystem.Application.Responses.Statistics;

public record ResultResponse(string Gid, double Score, double Percents, string FirstName, string LastName, string PollGid);