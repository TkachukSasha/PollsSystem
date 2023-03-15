namespace PollsSystem.Presentation.Polls.External.Requests;

public record SendRepliesRequest(Guid PollGid, string FirstName, string LastName, List<Guid> AnswerGids);