using Microsoft.EntityFrameworkCore;

namespace PollsSystem.Shared.Dal.Repositories;

public interface ITransactionalRepository
{
    ValueTask<TOut> ExecuteTransactionAsync<T1, T2, TOut>(Func<T1, T2, ValueTask<TOut>> operation, T1 firstParam, T2 secondParam);
}

public class TransactionalRepository<TContext> : ITransactionalRepository where TContext : DbContext
{
    private readonly TContext _dbContext;

    public TransactionalRepository(TContext dbContext)
        => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async ValueTask<TOut> ExecuteTransactionAsync<T1, T2, TOut>(Func<T1, T2, ValueTask<TOut>> operation, T1 firstParam, T2 secondParam)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.Execute(async () =>
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await operation(firstParam, secondParam);

                await transaction.CommitAsync();

                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                throw;
            }
        });
    }
}