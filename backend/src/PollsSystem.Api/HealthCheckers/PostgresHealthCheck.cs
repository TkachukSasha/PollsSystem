using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace PollsSystem.Api.HealthCheckers;

public class PostgresHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;

    public PostgresHealthCheck(IConfiguration configuration)
        => _configuration = configuration;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var connectionString = _configuration.GetSection("postgresDatabase")["postgresConnection"];

        try
        {
            using var connection = new NpgsqlConnection(connectionString);

            if (connection.State is not System.Data.ConnectionState.Open) connection.Open();

            if (connection.State is System.Data.ConnectionState.Open)
            {
                connection.Close();
                return Task.FromResult(HealthCheckResult.Healthy("The database is up and running."));
            }

            return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "The database is down."));
        }
        catch (Exception)
        {
            return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "The database is down."));
        }
    }
}