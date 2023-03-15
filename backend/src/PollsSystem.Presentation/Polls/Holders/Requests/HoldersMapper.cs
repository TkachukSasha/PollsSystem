using Mapster;
using PollsSystem.Application.Commands.Polls.Holder;

namespace PollsSystem.Presentation.Polls.Holders.Requests;

public class HoldersMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ChangeAnswerScoreRequest, ChangeAnswerScore>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<ChangeAnswerTextRequest, ChangeAnswerText>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<ChangePollDescriptionRequest, ChangePollDescription>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<ChangePollDurationRequest, ChangePollDuration>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<ChangePollKeyRequest, ChangePollKey>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<ChangePollTitleRequest, ChangePollTitle>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<ChangeQuestionTextRequest, ChangeQuestionText>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<CreatePollRequest, CreatePoll>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<CreatePollQuestionsRequest, CreatePollQuestions>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<DeletePollRequest, DeletePoll>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<DeletePollQuestionRequest, DeletePollQuestion>()
            .RequireDestinationMemberSource(true);
    }
}
