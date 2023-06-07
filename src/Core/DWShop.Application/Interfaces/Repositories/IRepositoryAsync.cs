using DWShop.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DWShop.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T, in TId> where T : class, IEntity<TId>
    {
        DbSet<T> Entities { get; }

        Task<T?> GetByIdAsync(TId id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPagedAsync(int pageNumber, int pageSize,
            Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy, params string[] IncludeArgs);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
