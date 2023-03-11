using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Shared.Default;

namespace PollsSystem.Shared.Api.Cors;

internal static class Extensions
{
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.BindOptions<CorsOptions>(DefaultConsts.Cors.SectionName);
        services.AddSingleton(options);

        return services.AddCors(cors =>
        {
            var allowedHeaders = options.AllowedHeaders;
            var allowedMethods = options.AllowedMethods;
            var allowedOrigins = options.AllowedOrigins;

            cors.AddPolicy(DefaultConsts.Cors.PolicyName, corsPolicy =>
            {
                var origins = allowedOrigins?.ToArray() ?? Array.Empty<String>();

                if (options.AllowCredentials && origins.FirstOrDefault() != "*")
                    corsPolicy.AllowCredentials();
                else
                    corsPolicy.DisallowCredentials();

                corsPolicy
                    .WithHeaders(allowedHeaders?.ToArray() ?? Array.Empty<string>())
                    .WithMethods(allowedMethods?.ToArray() ?? Array.Empty<string>())
                    .WithOrigins(origins.ToArray());
            });
        });
    }

    internal static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        => app.UseCors(DefaultConsts.Cors.PolicyName);
}

