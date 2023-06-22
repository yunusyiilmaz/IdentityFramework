using DataAccess.Repository.Abstract;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Project_Exam.Models;
using Project_Exam.Web.Hubs;
using Project_Exam.Web.Models;
using Service.Abstract;
using Service.Concrete;

namespace Project_Exam.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class CategoryController : Controller
    {
        private readonly MyHub _myHub;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IHubContext<MyHub, IMyHub> _hubContext;
        public CategoryController(IProductService productService, ICategoryService categoryService,IHubContext<MyHub, IMyHub> hubContext,MyHub myHub)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hubContext = hubContext;
            _myHub= myHub;
        }
        public async Task<IActionResult> Index(Guid categoryId)
        {
            var model = new CategoryListViewModel
            {
                Categories = categoryId != Guid.Empty ? await _categoryService.GetAsync(p => p.Id == categoryId) : await _categoryService.GetAsync()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            var categories = _categoryService.GetAsync().Result;
            var model = new AddCategoryViewModel();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel category)
        {          
            await _categoryService.AddAsync(new Entity.Category
            {
                Name = category.Name,
                IsActive=category.IsActive
            });
            await _hubContext.Clients.All.RealTime();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            var value = await _categoryService.DeleteByIdAsync(Guid.Parse(categoryId));
            var foo = value;
            await _hubContext.Clients.All.RealTime();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCategory(string categoryId)
        {
            var category = await _categoryService.GetSingleAsync(x => x.Id == Guid.Parse(categoryId));
            var categories = await _categoryService.GetAsync();
            var model = new AddCategoryViewModel()
            {
                Name = category.Name,

                Categories = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList()
            };
            return View("AddCategory", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(string categoryId, AddCategoryViewModel category)
        {
            await _categoryService.UpdateByIdAsync(new Entity.Category
            {
                Id = Guid.Parse(categoryId),
                Name = category.Name,

               //id = Guid.Parse(category.SelectedCategoryId)
            });
            await _hubContext.Clients.All.RealTime();
            return RedirectToAction("Index");
        }
    }
}
