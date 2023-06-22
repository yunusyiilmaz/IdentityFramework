using DataAccess.Context.Concrete;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Project_Exam.Models;
using Project_Exam.Web.Hubs;
using Project_Exam.Web.Models;
using Service.Abstract;
using Tweetinvi.Core.Events;
using Tweetinvi.Core.Models;

namespace Project_Exam.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class ProductController : Controller
    {
        private readonly MyHub _myHub;
        private readonly IHubContext<MyHub, IMyHub> _hubContext;
        private IProductService _productService;
        private ICategoryService _categoryService;


        public ProductController(IProductService productService, ICategoryService categoryService, MyHub myHub, IHubContext<MyHub, IMyHub> hubContext)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hubContext = hubContext;
            _myHub = myHub;
        }
        public async Task<IActionResult> Index(Guid categoryId)
        {
            var model = new ProductViewModel
            {

                products = categoryId != Guid.Empty ? await _productService.GetAsync(p => p.CategoryId == categoryId) :
                await _productService.GetAsync()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var categories = _categoryService.GetAsync().Result;
            var model = new AddProductViewModel();
            model.Categories = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAsync();
                var Pro = new AddProductViewModel
                {
                    Name = product.Name,
                    Price = product.Price,
                    UnitQuantity = product.UnitQuantity,
                    SelectedCategoryId = product.SelectedCategoryId.ToString(),
                    Categories = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString()}).ToList()
                };
                return View(Pro);
            }
            await _productService.AddAsync(new Entity.Product
            {
                Name = product.Name,
                Price = product.Price,
                UnitQuantity = product.UnitQuantity,
                CategoryId = Guid.Parse(product.SelectedCategoryId)
            });
            await _hubContext.Clients.All.RealTime();
            return RedirectToAction("Index");           
        }
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            var value = await _productService.DeleteByIdAsync(Guid.Parse(productId));
            await _hubContext.Clients.All.RealTime();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateProduct(string productId)
        {
            var product = await _productService.GetSingleAsync(x => x.Id == Guid.Parse(productId));
            var categories = await _categoryService.GetAsync();
            var model = new AddProductViewModel()
            {
                Name = product.Name,
                UnitQuantity = product.UnitQuantity,
                Price = product.Price,
                SelectedCategoryId = product.CategoryId.ToString(),
                Categories = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList()
            };
            return View("AddProduct", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(string productId, AddProductViewModel product)
        {
            await _productService.UpdateByIdAsync(new Entity.Product
            {
                Id = Guid.Parse(productId),
                Name = product.Name,
                Price = product.Price,
                UnitQuantity = product.UnitQuantity,
                CategoryId = Guid.Parse(product.SelectedCategoryId)
            });
            await _hubContext.Clients.All.RealTime();
            return RedirectToAction("Index");
        }

    }
}
