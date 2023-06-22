using DataAccess.Context.Concrete;
using DataAccess.Repository.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppIdentityDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithProductAsync()
        {
            var result = await _dbSet.Include(p => p.Products).ToListAsync();
            return result;
        }

        public async Task<List<Category>> GetCategoryInfoAsync(Expression<Func<Category, bool>> filter = null)
        {
            var result=await _dbSet.Include(c=>c.Id).ToListAsync();
            return result;
        }
    }
}
