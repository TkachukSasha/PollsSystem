using Carter;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Presentation.Users.Accounts.Requests;

namespace PollsSystem.Presentation;

public static class Extensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton(GetConfigureMappingConfig());

        services.AddScoped<IMapper, ServiceMapper>();

        services.AddCarter();

        return services;
    }

    public static void UsePresentation(this WebApplication app)
    {
        app.MapCarter();
    }

    private static TypeAdapterConfig GetConfigureMappingConfig()
    {
        var config = new TypeAdapterConfig();

        new AccountsMapper().Register(config);

        return config;
    }
}