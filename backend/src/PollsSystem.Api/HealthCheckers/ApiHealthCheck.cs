using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PollsSystem.Api.HealthCheckers;

public class ApiHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiHealthCheck(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        using var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync("http://localhost:5000/api/v1/external/ping");

        if (response.IsSuccessStatusCode) return await Task.FromResult(new HealthCheckResult(status: HealthStatus.Healthy, description: "The API is up and running."));

        return await Task.FromResult(new HealthCheckResult(status: HealthStatus.Unhealthy, description: "The API is down."));
    }
}
