namespace PollsSystem.Presentation.Polls.Holders.Requests;

public record ChangeAnswerScoreRequest(string QuestionGid, string AnswerGid, string ScoreGid);

public record ChangeAnswerTextRequest(string QuestionGid, string AnswerGid, string Text);

public record ChangePollDescriptionRequest(string PollGid, string Description);

public record ChangePollDurationRequest(string PollGid, int Duration);

public record ChangePollKeyRequest(string PollGid, string CurrentKey);

public record ChangePollTitleRequest(string PollGid, string Title);

public record ChangeQuestionTextRequest(string QuestionGid, string Question);

public record CreatePollRequest(string Title, string Description, int NumberOfQuestions, int Duration, string AuthorGid);

public record CreatePollQuestionsRequest(string PollGid, List<QuestionDto> Questions);

public record DeletePollRequest(string PollGid);

public record DeletePollQuestionRequest(string PollGid, string QuestionGid);

public record DeleteQuestionAnswerRequest(string QuestionGid, string AnswerGid);

public record GetPollQuery(string PollGid);

public record GetUserPollsQuery(string UserGid);

public record GetPollByTitleQuery(string Title);

public record QuestionDto(string QuestionName, List<AnswerDto> Answers);

public record AnswerDto(string AnswerText, Guid ScoreGid);