using Mapster;
using PollsSystem.Application.Commands.Statistics.Results;

namespace PollsSystem.Presentation.Statistics.Results.Requests;

public class ResultsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ChangeResultScoreRequest, ChangeResultScore>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<DeleteResultRequest, DeleteResult>()
            .RequireDestinationMemberSource(true);
    }
}