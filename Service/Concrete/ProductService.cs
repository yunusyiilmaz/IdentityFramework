using DataAccess.Repository.Abstract;
using Entity;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productService)
        {
            _productRepository = productService;
        }

        public Task<bool> AddAsync(Product entity)
        {
            return _productRepository.AddAsync(entity);
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            return _productRepository.DeleteByIdAsync(id);
        }

        public Task<List<Product>> GetAsync(Expression<Func<Product, bool>> filter = null)
        {
            return _productRepository.GetAsync(filter);
        }

        public async Task<List<Product>> GetProductsWithCategoryInfoAsync(Expression<Func<Product, bool>> filter = null)
        {
            return await _productRepository.GetProductsWithCategoryInfoAsync(filter);
        }

        public Task<Product> GetSingleAsync(Expression<Func<Product, bool>> filter = null)
        {
            return _productRepository.GetSingleAsync(filter);
        }

        public Task<Product> UpdateByIdAsync(Product entity)
        {
            return _productRepository.UpdateByIdAsync(entity);
        }
    }
}
