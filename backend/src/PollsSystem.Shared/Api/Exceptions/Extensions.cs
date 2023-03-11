using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Shared.Api.Exceptions.Mappers;
using PollsSystem.Shared.Api.Exceptions.Middlewars;

namespace PollsSystem.Shared.Api.Exceptions;

internal static class Extensions
{
    internal static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services
                 .AddSingleton<ErrorHandlerMiddleware>()
                 .AddSingleton<IExceptionMapper, ExceptionMapper>();

    internal static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlerMiddleware>();
}