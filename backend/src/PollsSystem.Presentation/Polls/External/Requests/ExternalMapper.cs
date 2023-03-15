using Mapster;
using PollsSystem.Application.Commands.Polls.External;

namespace PollsSystem.Presentation.Polls.External.Requests;

public class ExternalMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SendRepliesRequest, SendReplies>()
             .RequireDestinationMemberSource(true);
    }
}