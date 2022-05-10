using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                         string includeProperties = "", int? skip = null, int? take = null);
        Task<T> GetFirstAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsync(params object[] keyValues);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        void Delete(T entity);
    }
}
