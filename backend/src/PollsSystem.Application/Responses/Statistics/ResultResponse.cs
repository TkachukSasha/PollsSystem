namespace PollsSystem.Application.Responses.Statistics;

public record ResultResponse(Guid Gid, double Score, double Percents, string FirstName, string LastName, Guid PollGid);