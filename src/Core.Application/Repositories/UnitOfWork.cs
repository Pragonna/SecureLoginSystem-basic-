using Microsoft.EntityFrameworkCore;

namespace Core.Application.Repositories
{
    public class UnitOfWork<TContext>(TContext context) : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
