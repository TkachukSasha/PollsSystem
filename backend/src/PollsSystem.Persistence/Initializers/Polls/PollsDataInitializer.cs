using Microsoft.Extensions.Logging;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Dal.Initializers;

namespace PollsSystem.Persistence.Initializers.Polls;

public class PollsDataInitializer : IDataInitializer
{
    private readonly PollsSystemDbContext _dbContext;
    private readonly ILogger<PollsDataInitializer> _logger;

    public PollsDataInitializer(
        PollsSystemDbContext dbContext,
        ILogger<PollsDataInitializer> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InitAsync()
    {
        if (_dbContext.Scores.Any()) return;

        await AddScoresAsync();

        await _dbContext.SaveChangesAsync();
    }

    private async Task AddScoresAsync()
    {
        await _dbContext.Scores.AddAsync(Score.Init(0.0d));
        await _dbContext.Scores.AddAsync(Score.Init(0.25d));
        await _dbContext.Scores.AddAsync(Score.Init(0.50d));
        await _dbContext.Scores.AddAsync(Score.Init(0.75d));
        await _dbContext.Scores.AddAsync(Score.Init(1.0d));

        _logger.LogInformation("Initialized scores");
    }
}
