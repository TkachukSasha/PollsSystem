using PollsSystem.Application.Dto;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Channels;

namespace PollsSystem.Application.Common.Channels;

public interface ISendRepliesChannel
{
    ValueTask AppyResultAsync(UserReply model, CancellationToken cancellationToken);
    ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken);
    bool TryRead([MaybeNullWhen(false)] out UserReply model, CancellationToken cancellationToken);
}

public class SendRepliesChannel : ISendRepliesChannel
{
    private readonly Channel<UserReply> _channel;

    public SendRepliesChannel()
    {
        _channel = Channel.CreateUnbounded<UserReply>();
    }

    public async ValueTask AppyResultAsync(UserReply model, CancellationToken cancellationToken)
        => await _channel.Writer.WriteAsync(model, cancellationToken);

    public async ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken)
        => await _channel.Reader.WaitToReadAsync(cancellationToken);

    public bool TryRead([MaybeNullWhen(false)] out UserReply model, CancellationToken cancellationToken)
        => _channel.Reader.TryRead(out model);
}
