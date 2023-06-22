
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Project_Exam.Models;
using Service.Abstract;

namespace Project_Exam.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        private ICategoryService _categoryServices;
        public CategoryListViewComponent(ICategoryService category)
        {
            _categoryServices = category;
        }


        public ViewViewComponentResult Invoke()
        {
            var model = new CategoryListViewModel
            {
                Categories = _categoryServices.GetAsync().Result
            };
            return View(model);
        }
    }
}
