using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PollsSystem.Shared.Dal.Initializers;
using PollsSystem.Shared.Dal.Options;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;

namespace PollsSystem.Shared.Dal;

public static class Extensions
{
    public static IServiceCollection AddPostgresDatabase<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.ConfigureOptions<PostgresOptionsSetup>();

        services.AddDbContext<TContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<PostgresOptions>>()!.Value;

            if (databaseOptions is not null)
                dbContextOptionsBuilder.UseNpgsql(databaseOptions.PostgresConnection);
        });

        services.AddHostedService<DatabaseInitializer<TContext>>();
        services.AddHostedService<DataInitializer>();

        services.AddScoped<ITransactionalRepository, TransactionalRepository<TContext>>();

        services.AddScoped<IBaseRepository, BaseRepository<TContext>>();
        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IDataInitializer
         => services.AddTransient<IDataInitializer, T>();
}