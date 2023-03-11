using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PollsSystem.Shared.Default;

namespace PollsSystem.Shared.Dal.Options;

internal sealed class PostgresOptionsSetup : IConfigureOptions<PostgresOptions>
{
    private readonly IConfiguration _configuration;

    public PostgresOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(PostgresOptions options)
    {
        if (options is not null)
        {
            var connectionString = _configuration.GetConnectionString(DefaultConsts.Dal.ConfigurationSectionPostgresConnection);

            if (!string.IsNullOrWhiteSpace(connectionString))
                options.PostgresConnection = connectionString;

            _configuration.GetSection(DefaultConsts.Dal.ConfigurationSectionPostgresName).Bind(options);
        }
    }
}