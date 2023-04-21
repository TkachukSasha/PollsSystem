namespace PollsSystem.Application.Responses.Polls;

public record PollResponse(Guid Gid, string Title, string Description, int NumberOfQuestions, int Duration, string Key);

public record QuestionsWithAnswersDefaultResponse(Guid Gid, string QuestionName, List<AnswerDefaultResponse> Answers);

public record AnswerDefaultResponse(Guid Gid, string AnswerName);

public record QuestionsWithAnswersAndScoresResponse(Guid Gid, string QuestionName, List<AnswerWithScoresResponse> Answers);

public record AnswerWithScoresResponse(Guid Gid, string AnswerName, string ScoreGid);

public record ScoreResponse(Guid ScoreGid, double ScoreValue);