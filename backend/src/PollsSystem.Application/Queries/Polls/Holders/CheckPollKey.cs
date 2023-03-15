using Mediator;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;

namespace PollsSystem.Application.Queries.Polls.Holders;

public sealed record CheckPollKey(Guid PollGid, string Key) : IQuery<bool>;

public class CheckPollKeyHandler : IQueryHandler<CheckPollKey, bool>
{
    private readonly IBaseRepository _baseRepository;

    public CheckPollKeyHandler(IBaseRepository baseRepository)
        => _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));

    public async ValueTask<bool> Handle(CheckPollKey query, CancellationToken cancellationToken)
    {
        var existingPoll = await _baseRepository.GetByConditionAsync<Poll>(x => x.Gid == query.PollGid);

        if (existingPoll is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Poll with: {query.PollGid} is null!");

        return existingPoll.Key == query.Key;
    }
}