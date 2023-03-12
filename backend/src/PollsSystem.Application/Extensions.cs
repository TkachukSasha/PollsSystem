using Mediator;
using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Application.Commands.Validation.Pipeline;

namespace PollsSystem.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        return services
                .AddSingleton(typeof(IPipelineBehavior<,>), typeof(CommandValidatorBehaviour<,>));
    }
}