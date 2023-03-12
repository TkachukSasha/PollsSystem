using Microsoft.Extensions.Logging;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Dal.Initializers;

namespace PollsSystem.Persistence.Initializers.Polls;

public class PollsDataInitializer : IDataInitializer
{
    private readonly PollsSystemDbContext _pollsDbContext;
    private readonly ILogger<PollsDataInitializer> _logger;

    public PollsDataInitializer(
        PollsSystemDbContext pollsDbContext,
        ILogger<PollsDataInitializer> logger)
    {
        _pollsDbContext = pollsDbContext ?? throw new ArgumentNullException(nameof(pollsDbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InitAsync()
    {
        if (_pollsDbContext.Scores.Any()) return;

        await AddScoresAsync();

        await _pollsDbContext.SaveChangesAsync();
    }

    private async Task AddScoresAsync()
    {
        await _pollsDbContext.Scores.AddAsync(Score.Init(0.0d));
        await _pollsDbContext.Scores.AddAsync(Score.Init(0.25d));
        await _pollsDbContext.Scores.AddAsync(Score.Init(0.50d));
        await _pollsDbContext.Scores.AddAsync(Score.Init(0.75d));
        await _pollsDbContext.Scores.AddAsync(Score.Init(1.0d));

        _logger.LogInformation("Initialized scores");
    }
}
