using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Shared.Dal.Initializers;
using PollsSystem.Shared.Security.Cryptography;

namespace PollsSystem.Persistence.Initializers.Users;

public class UsersDataInitializer : IDataInitializer
{
    private readonly PollsSystemDbContext _dbContext;
    private readonly IPasswordManager _passwordManager;
    private readonly ILogger<UsersDataInitializer> _logger;

    public UsersDataInitializer(
        PollsSystemDbContext dbContext,
        IPasswordManager passwordManager,
        ILogger<UsersDataInitializer> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
        _logger = logger;
    }

    public async Task InitAsync()
    {
        if (await _dbContext.Roles.AnyAsync()) return;

        await AddRolesAsync();

        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRolesAsync()
    {
        await _dbContext.Roles.AddAsync(Role.Init(Role.Admin, true));

        await _dbContext.Roles.AddAsync(Role.Init(Role.DefaultRole, true));

        _logger.LogInformation("Initialized roles.");
    }
}