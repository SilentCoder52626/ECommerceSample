using ECommerce.Dto;
using ECommerce.Repository;
using ECommerce.Service;
using ECommerceSample.Areas.Product.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSample.Areas.Product.Controllers
{
    [Area("Product")]
    public class ProductController : Controller
    {
        private readonly ProductRepositoryInterface _productRepo;
        private readonly ILogger<ProductController> _logger;
        private readonly BrandRepositoryInterface _brandRepo;
        private readonly TagRepositoryInterface _tagRepo;
        private readonly CategoryRepositoryInterface _categoryRepo;
        private readonly IWebHostEnvironment _environment;
        private readonly ProductServiceInterface _productService;


        public ProductController(ProductRepositoryInterface productRepo, ILogger<ProductController> logger, BrandRepositoryInterface brandRepo, TagRepositoryInterface tagRepo, CategoryRepositoryInterface categoryRepo, IWebHostEnvironment environment, ProductServiceInterface productService)
        {
            _productRepo = productRepo;
            _logger = logger;
            _brandRepo = brandRepo;
            _tagRepo = tagRepo;
            _categoryRepo = categoryRepo;
            _environment = environment;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var Models = new List<ProductIndexViewModel>();
            var Products = await _productRepo.GetQueryable().OrderBy(a => a.ProductId).ToListAsync();
            foreach(var product in Products)
            {
                Models.Add(new ProductIndexViewModel()
                {
                    Id = product.ProductId,
                    Name = product.Name,
                    Brand = product.Brand.Name,
                    BrandId = product.BrandId,
                    Category = product.Category.Name,
                    CategoryId = product.CategoryId,
                    Color = product.Color,
                    Description = product.Description,
                    Image = product.Image,
                    SKU = product.SKU,
                    Price = product.Price,
                    Tag = product.Tag.Name,
                    TagId = product.TagId,
                    AvailibilityStatus = product.AvailabilityStatus
                });
            }
            return View(Models);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductCreateViewModel Model = new ProductCreateViewModel();
            Model.Brands = await _brandRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
            Model.Categories = await _categoryRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
            Model.Tags = await _tagRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model,IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                #region Image Upload
                var path = Path.Combine(_environment.WebRootPath, "images/");
                if ((!Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = file.FileName;
                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                var ImagePath = $"/images/{fileName}";

                # endregion
                var Dto = new ProductCreateDto()
                {
                    Name = model.Name,
                    BrandId = model.BrandId,
                    CategoryId = model.CategoryId,
                    Color = model.Color,
                    Description = model.Description,
                    Image = ImagePath,
                    Price = model.Price,
                    SKU = model.SKU,
                    TagId = model.TagId

                };
                var Product = await _productService.Create(Dto);

            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
