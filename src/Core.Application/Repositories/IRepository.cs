using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Application.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> RemoveAsync(T entity);
    }

    public interface IReadRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                         bool enableTracking = false);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    }
}
