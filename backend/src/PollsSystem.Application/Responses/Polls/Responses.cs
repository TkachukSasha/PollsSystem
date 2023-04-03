namespace PollsSystem.Application.Responses.Polls;

public record PollResponse(Guid Gid, string Title, string Description, int NumberOfQuestions, int Duration);

public record QuestionsWithAnswersResponse(Guid Gid, string QuestionName, List<AnswerResponse> Answers);

public record AnswerResponse(Guid Gid, string AnswerName);

public record ScoreResponse(Guid Gid, double Value);