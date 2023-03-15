namespace PollsSystem.Presentation.Polls.Holders.Requests;

public record ChangeAnswerScoreRequest(Guid QuestionGid, Guid AnswerGid, Guid ScoreGid);

public record ChangeAnswerTextRequest(Guid QuestionGid, Guid AnswerGid, string Text);

public record ChangePollDescriptionRequest(Guid PollGid, string Description);

public record ChangePollDurationRequest(Guid PollGid, int Duration);

public record ChangePollKeyRequest(Guid PollGid, string CurrentKey);

public record ChangePollTitleRequest(Guid PollGid, string CurrentTitle, string Title);

public record ChangeQuestionTextRequest(string CurrentQuestion, string Question);

public record CreatePollRequest(string Title, string Description, int NumberOfQuestions, int Duration, Guid AuthorGid);

public record CreatePollQuestionsRequest(Guid PollGid, List<QuestionDto> Questions);

public record DeletePollRequest(Guid PollGid);

public record DeletePollQuestionRequest(Guid PollGid, Guid QuestionGid);

public record GetPollQuery(Guid PollGid);

public record GetPollByTitleQuery(string Title);

public record QuestionDto(string QuestionName, List<AnswerDto> Answers);

public record AnswerDto(string AnswerText, Guid ScoreGid);