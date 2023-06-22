using Entity;
using System.Linq.Expressions;

namespace DataAccess.Repository.Abstract
{
    public interface IProductRepository : IBaseRepositoryAsync<Product>
    {
        public Task<List<Product>> GetProductsWithCategoryInfoAsync(Expression<Func<Product, bool>> filter = null);
    }
}
