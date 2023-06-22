using Core.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Repository.Abstract
{
    public interface IBaseRepositoryAsync<T> where T : BaseEntity
    {
        public Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null);
        public Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null);
        public Task<bool> AddAsync(T entity);
        public Task<T> UpdateByIdAsync(T entity);
        public Task<bool> DeleteByIdAsync(Guid id);
    }
}
