using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Application.Repositories
{
    public abstract class BaseRepository<T, TContext> : IRepository<T>, IReadRepository<T>
        where T : Entity
        where TContext : DbContext
    {
        protected readonly TContext _context;
        public BaseRepository(TContext context)
        {
            _context = context;
        }
        private IQueryable<T> Query => _context.Set<T>();
        public Task<T> AddAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            return Task.FromResult(entity);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await Query.FirstOrDefaultAsync(expression);
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool enableTracking = false)
        {
            IQueryable<T> query = Query;
            if (expression != null)
                query = query.Where(expression);

            if (include != null)
                query = include(query);

            if (!enableTracking)
                query = query.AsNoTracking();

            return Task.FromResult(query);    
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<T> RemoveAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }
    }
}
