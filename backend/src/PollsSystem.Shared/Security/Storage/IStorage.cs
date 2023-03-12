using Microsoft.AspNetCore.Http;

namespace PollsSystem.Shared.Security.Storage;

public interface IStorage
{
    void Set<TEntity>(TEntity entity, string key);
    object Get<TEntity>(string key);
}

public class HttpStorage : IStorage
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpStorage(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    public void Set<TEntity>(TEntity entity, string key)
        => _httpContextAccessor.HttpContext?.Items.TryAdd(key, entity);

    public object Get<TEntity>(string key)
    {
        if (_httpContextAccessor.HttpContext is null) return null;

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(key, out var entity)) return entity;

        return null;
    }
}
