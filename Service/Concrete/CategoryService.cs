using DataAccess.Repository.Abstract;
using DataAccess.Repository.Concrete;
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
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<bool> AddAsync(Category entity)
        {
            return _categoryRepository.AddAsync(entity);
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            return _categoryRepository.DeleteByIdAsync(id);
        }

        public Task<List<Category>> GetAsync(Expression<Func<Category, bool>> filter = null)
        {
            return _categoryRepository.GetAsync(filter);
        }

        public async Task<List<Category>> GetCategoryInfoAsync(Expression<Func<Category, bool>> filter = null)
        {
            return await _categoryRepository.GetCategoryInfoAsync(filter);
        }

        public Task<Category> GetSingleAsync(Expression<Func<Category, bool>> filter = null)
        {
            return _categoryRepository.GetSingleAsync(filter);
        }

        public Task<Category> UpdateByIdAsync(Category entity)
        {
            return _categoryRepository.UpdateByIdAsync(entity);
        }

        //public Task<List<Category>> GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Category>> GetByCategoryAsync(Guid categoryId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Category> GetById(Guid id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
