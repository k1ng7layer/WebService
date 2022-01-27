using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interfaces
{
    public interface IRepository<T> where T:class
    {
        IQueryable<T> GetAll();
        
        Task<T> GetAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task CreateAsync(T item);
        
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);

        void DeleteAllByUserId(int userId);
        Task AddRangeAsync(IEnumerable<T> entities);

    }
}

