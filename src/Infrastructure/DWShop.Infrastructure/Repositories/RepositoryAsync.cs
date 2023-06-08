using System.Linq.Expressions;
using DWShop.Application.Interfaces.Repositories;
using DWShop.Domain.Contracts;
using DWShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Repositories
{
    public class RepositoryAsync<T, TId>: IRepositoryAsync<T,TId> where T: AuditableEntity<TId>
    {
        private readonly DWShopContext _context;

        public RepositoryAsync(DWShopContext context)
        {
            _context = context;
        }

        public DbSet<T> Entities { get; }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _context.Set<T>()
                                 .FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                                 .ToListAsync();
        }

        public async Task<List<T>> GetPagedAsync(int pageNumber, int pageSize, 
            Expression<Func<T, bool>> predicate, Func<IQueryable<T>, 
                IOrderedQueryable<T>> orderBy, params string[] IncludeArgs)
        {
            IQueryable<T> query = _context.Set<T>();

            // agregamos los joins
            query = IncludeArgs.Aggregate(query, (current, ItemInclude)
                => current.Include(ItemInclude));
            
            // si hubio predicado (where) lo agregamos
            if (predicate is not null)
                query = query.Where(predicate);

            // paginacion
            query = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // regresamos ordenado o no
            return await (orderBy is not null ? orderBy(query).ToListAsync() : query.ToListAsync());

        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            T entitySaved = await _context.Set<T>().FindAsync(entity.Id);
            _context.Entry(entitySaved!).CurrentValues.SetValues(entity);
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => 
            await _context.SaveChangesAsync(CancellationToken.None);
    }
}
