using Coco.Core.Interfaces;
using Coco.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Coco.Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        private IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null,
                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                         string includeProperties = "", int? skip = null, int? take = null)
        {
            IQueryable<T> query = _entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                          string includeProperties = "", int? skip = null, int? take = null)
        {
            var query = GetQueryable(filter, orderBy, includeProperties, skip, take);
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            return await GetQueryable(filter, null, includeProperties).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(params object[] keyValues)
        {
            return await _entities.FindAsync(keyValues);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
        {
            await _entities.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            T entity = await GetByIdAsync(id);
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}
