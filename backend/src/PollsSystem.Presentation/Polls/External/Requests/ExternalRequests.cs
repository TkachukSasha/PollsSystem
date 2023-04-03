namespace PollsSystem.Presentation.Polls.External.Requests;

public record SendRepliesRequest(string PollGid, string FirstName, string LastName, Dictionary<string, string> QuestionAnswer);