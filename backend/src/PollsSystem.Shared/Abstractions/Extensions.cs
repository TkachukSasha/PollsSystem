using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Shared.Abstractions.Time;

namespace PollsSystem.Shared.Abstractions;

internal static class Extensions
{
    internal static IServiceCollection AddAbstractions(this IServiceCollection services)
    => services
            .AddScoped<IUtcClock, UtcClock>();
}