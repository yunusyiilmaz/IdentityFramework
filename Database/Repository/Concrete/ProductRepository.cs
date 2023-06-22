using DataAccess.Context.Concrete;
using DataAccess.Repository.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

       
        public ProductRepository(AppIdentityDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Product>> GetProductsWithCategoryInfoAsync(Expression<Func<Product, bool>> filter = null)
        {
            return filter == null ? await _dbSet.Include(p => p.Category).ToListAsync() : await _dbSet.Where(filter).Include(p => p.Category).ToListAsync();
        }
    }
}
