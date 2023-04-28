using HealthChecks.UI.Client;
using Microsoft.AspNetCore.RateLimiting;
using PollsSystem.Api.HealthCheckers;
using PollsSystem.Application;
using PollsSystem.Persistence;
using PollsSystem.Presentation;
using PollsSystem.Shared;
using System.Text.Json.Serialization;

namespace PollsSystem.Api.DependencyInjection;

public static class Extensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplication();
        builder.Services.AddPersistence();
        builder.Services.AddPresentation();

        builder.Services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            rateLimiterOptions.AddSlidingWindowLimiter("sliding", options =>
            {
                options.PermitLimit = 10;
                options.Window = TimeSpan.FromSeconds(10);
                options.SegmentsPerWindow = 2;
                options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 5;
            });
        });

        builder.Services
            .AddHealthChecks()
            .AddCheck<ApiHealthCheck>(nameof(ApiHealthCheck))
            .AddCheck<PostgresHealthCheck>(nameof(PostgresHealthCheck));

        builder.Services
            .AddHealthChecksUI(options =>
            {
                options.AddHealthCheckEndpoint("Healthcheck API", "/healthcheck");
            })
            .AddInMemoryStorage();

        return builder;
    }

    public static void AddApplication(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UsePresentation();

        app.MapHealthChecks("/healthcheck", new()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecksUI(options => options.UIPath = "/dashboard");

        app.UseShared();

        app.Run();
    }
}