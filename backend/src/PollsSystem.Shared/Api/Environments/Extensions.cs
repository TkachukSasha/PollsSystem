using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace PollsSystem.Shared.Api.Environments;

internal static class Extensions
{
    internal static IConfiguration SetEnvironmentConfiguration(this IConfiguration configuration, IWebHostEnvironment environment)
    {
        return new ConfigurationBuilder()
            .SetBasePath(configuration.GetSection("contentRoot").Value)
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables().Build();
    }

    internal static string GetCurrentEnvironment(IWebHostEnvironment environment)
        => $"Environment: {environment.EnvironmentName}";
}