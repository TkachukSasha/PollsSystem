using Microsoft.Extensions.DependencyInjection;
using PollsSystem.Domain.Repositories;
using PollsSystem.Persistence.Initializers.Polls;
using PollsSystem.Persistence.Initializers.Users;
using PollsSystem.Persistence.Repositories;
using PollsSystem.Shared.Dal;

namespace PollsSystem.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
        => services
                   .AddScoped<IQuestionRepository, QuestionRepository>()
                   .AddPostgresDatabase<PollsSystemDbContext>()
                   .AddInitializer<UsersDataInitializer>()
                   .AddInitializer<PollsDataInitializer>();
}