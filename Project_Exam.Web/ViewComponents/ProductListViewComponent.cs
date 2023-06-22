
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Project_Exam.Models;
using Service.Abstract;

namespace Project_Exam.ViewComponents
{
    public class ProductListViewComponent:ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductListViewComponent(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }


        public ViewViewComponentResult Invoke()
        {
            if(!Guid.TryParse(HttpContext.Request.Query["categoryId"].FirstOrDefault(), out var categoryId))
                categoryId = _categoryService.GetSingleAsync().Result.Id;
            var model = new ProductListViewModel
            {
                Products = _productService.GetProductsWithCategoryInfoAsync(p => p.CategoryId == categoryId).Result
            };
            return View(model);
        }
    }
}
