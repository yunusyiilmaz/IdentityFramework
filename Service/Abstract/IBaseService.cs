using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IBaseService<T>
    {
        public Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null);
        public Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null);
        public Task<bool> AddAsync(T entity);
        public Task<T> UpdateByIdAsync(T entity);
        public Task<bool> DeleteByIdAsync(Guid id);
    }
}
