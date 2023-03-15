using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Presentation.Polls.External.Requests;
using PollsSystem.Presentation.Polls.Holders.Requests;
using PollsSystem.Presentation.Statistics.Results.Requests;
using PollsSystem.Presentation.Users.Accounts.Requests;
using PollsSystem.Presentation.Users.Roles.Requests;
using PollsSystem.Presentation.Users.UsersManagement.Requests;

namespace PollsSystem.Presentation;

public static class Extensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton(GetConfigureMappingConfig());

        services.AddScoped<IMapper, ServiceMapper>();

        services.AddControllers();

        return services;
    }

    public static void UsePresentation(this WebApplication app)
    {
        app.MapControllers();
    }

    private static TypeAdapterConfig GetConfigureMappingConfig()
    {
        var config = new TypeAdapterConfig();

        // TODO: register scope
        new AccountsMapper().Register(config);
        new RolesMapper().Register(config);
        new HoldersMapper().Register(config);
        new ExternalMapper().Register(config);
        new ResultsMapper().Register(config);
        new UsersManagementMapper().Register(config);

        return config;
    }
}