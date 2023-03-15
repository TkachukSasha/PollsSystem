namespace PollsSystem.Application.Dto;

public sealed record UserReply(double FinalScore, double Percents, string FirstName, string LastName, Guid PollGid);