using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Shared.Abstractions;
using PollsSystem.Shared.Api.Cors;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Default;
using PollsSystem.Shared.Observability.Logging;
using PollsSystem.Shared.Security;

namespace PollsSystem.Shared;

public static class Extensions
{
    public static WebApplicationBuilder AddShared(this WebApplicationBuilder builder)
    {
        var appOptions = builder.Configuration.GetSection("app").BindOptions<AppOptions>();
        var appInfo = new AppInfo(appOptions.Name, appOptions.Version);
        builder.Services.AddSingleton(appInfo);

        //builder.Configuration.SetEnvironmentConfiguration(builder.Environment);

        builder
            .AddLogging()
            .Services
            .AddErrorHandling()
            .AddJwt(builder.Configuration)
            .AddCorsPolicy(builder.Configuration)
            .AddHttpContextAccessor()
            .AddAbstractions()
            .AddLogger(builder.Configuration);

        return builder;
    }

    public static WebApplication UseShared(this WebApplication app)
    {
        app
           .UseCorsPolicy()
           .UseErrorHandling()
           .UseAuthentication()
           .UseRouting()
           .UseAuthorization()
           .UseResponseCaching();

        return app;
    }
}