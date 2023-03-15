using Microsoft.Extensions.DependencyInjection;

namespace PollsSystem.Application.Common.Utils;

public interface IServiceScopeFactory<T> where T : class
{
    T Get();
}

public class ServiceScopeFactory<T> : IServiceScopeFactory<T>
    where T : class
{
    private readonly IServiceScopeFactory _serviceSCopeFactory;

    public ServiceScopeFactory(IServiceScopeFactory serviceScopeFactory)
        => _serviceSCopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));

    public T Get() => _serviceSCopeFactory.CreateScope().ServiceProvider.GetRequiredService<T>();
}
