using Microsoft.Extensions.Hosting;
using PollsSystem.Application.Commands.Statistics.Results;
using PollsSystem.Application.Common.Channels;
using PollsSystem.Application.Common.Utils;
using PollsSystem.Application.Dto;

namespace PollsSystem.Application.Common.BackgroundServices;

public class SendRepliesBackgroundService : BackgroundService
{
    private readonly ISendRepliesChannel _channel;
    private readonly IServiceScopeFactory<CreateResultHandler> _createResultHandler;

    public SendRepliesBackgroundService(
        ISendRepliesChannel channel,
        IServiceScopeFactory<CreateResultHandler> createResultHandler)
    {
        _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        _createResultHandler = createResultHandler ?? throw new ArgumentNullException(nameof(createResultHandler));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await _channel.WaitToReadAsync(cancellationToken);

            if (!_channel.TryRead(out var reply, cancellationToken))
                continue;

            CreateResult command = reply.ToCreateResult();

            var createResultHandler = _createResultHandler.Get();

            await createResultHandler.Handle(command, cancellationToken);
        }
    }
}

public static class Mapper
{
    public static CreateResult ToCreateResult(this UserReply model)
        => new CreateResult(
            model.FinalScore,
            model.Percents,
            model.FirstName,
            model.LastName,
            model.PollGid
           );
}