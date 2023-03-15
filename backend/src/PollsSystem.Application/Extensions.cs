using Mediator;
using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Application.Commands.Statistics.Results;
using PollsSystem.Application.Commands.Validation.Pipeline;
using PollsSystem.Application.Common.BackgroundServices;
using PollsSystem.Application.Common.Channels;
using PollsSystem.Application.Common.Services;
using PollsSystem.Application.Common.Utils;

namespace PollsSystem.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IStatisticCalculationService, StatisticCalculationService>();
        services.AddSingleton<ISendRepliesChannel, SendRepliesChannel>();
        services.AddHostedService<SendRepliesBackgroundService>();
        services.AddSingleton<IServiceScopeFactory<CreateResultHandler>, ServiceScopeFactory<CreateResultHandler>>();

        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        return services
                .AddSingleton(typeof(IPipelineBehavior<,>), typeof(CommandValidatorBehaviour<,>));
    }
}