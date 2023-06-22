using Core.Concrete;
using DataAccess.Context.Concrete;
using DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Concrete
{
    public class Repository<T> : IBaseRepositoryAsync<T> where T : BaseEntity
    {
        protected readonly AppIdentityDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
                return await _dbSet.OrderBy(e => e.CreatDate).FirstOrDefaultAsync();
            return await _dbSet.SingleOrDefaultAsync(filter);
        }
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? await _dbSet.ToListAsync() : await _dbSet.Where(filter).ToListAsync();
        }
        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<T> UpdateByIdAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteByIdAsync(Guid id)
        {

            var entity = await GetSingleAsync(p => p.Id == id);
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
