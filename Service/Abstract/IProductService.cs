using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IProductService : IBaseService<Product>
    {
        public Task<List<Product>> GetProductsWithCategoryInfoAsync(Expression<Func<Product, bool>> filter = null);
    }
}
