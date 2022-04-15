using ECommerce.Repository;
using ECommerceSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerceSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepositoryInterface _productRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ProductRepositoryInterface productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }

        public async Task<IActionResult> Index(HomeViewModel model)
        {
            var Products = await _productRepo.GetQueryable().Include(a => a.Brand).Include(a => a.Category).Include(a => a.Tag).OrderBy(a => a.Name).ToListAsync();
            model.ProductListModel = Products.Select(a => new ProductListModel()
            {
                Image = a.Image,
                Name = a.Name,
                Price = a.Price,
                Id = a.ProductId
            }).ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public async Task<IActionResult> Details(long productId)
        {
            var Product = await _productRepo.GetQueryable().Include(a => a.Brand).Include(a => a.Category).Include(a => a.Tag).OrderBy(a => a.Name).SingleOrDefaultAsync(a => a.ProductId == productId);
            var model = new ProductDetailViewModel()
            {
                ProductId = Product.ProductId,
                AvailabilityStatus = Product.AvailabilityStatus,
                BrandName = Product.Brand.Name,
                CategoryName = Product.Category.Name,
                Color = Product.Color,
                Description = Product.Description,
                Image = Product.Image,
                Name = Product.Name,
                SKU = Product.SKU,
                Price = Product.Price,
                TagName = Product.Tag.Name
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}