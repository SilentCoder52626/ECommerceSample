using AspNetCoreHero.ToastNotification.Abstractions;
using ECommerce.Dto;
using ECommerce.Exceptions;
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
        private readonly INotyfService _notify;

        public ProductController(ProductRepositoryInterface productRepo, ILogger<ProductController> logger, BrandRepositoryInterface brandRepo, TagRepositoryInterface tagRepo, CategoryRepositoryInterface categoryRepo, IWebHostEnvironment environment, ProductServiceInterface productService, INotyfService notify)
        {
            _productRepo = productRepo;
            _logger = logger;
            _brandRepo = brandRepo;
            _tagRepo = tagRepo;
            _categoryRepo = categoryRepo;
            _environment = environment;
            _productService = productService;
            _notify = notify;
        }

        public async Task<IActionResult> Index()
        {
            var Models = new List<ProductIndexViewModel>();
            var Products = await _productRepo.GetQueryable().Include(x => x.Category).Include(y => y.Brand).Include(y => y.Tag).OrderBy(a => a.ProductId).ToListAsync();
            foreach (var product in Products)
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
            await ConfigureCreateDropDownList(Model);
            return View(Model);
        }

        private async Task ConfigureCreateDropDownList(ProductCreateViewModel Model)
        {
            Model.Brands = await _brandRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
            Model.Categories = await _categoryRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
            Model.Tags = await _tagRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
        }
        private async Task ConfigureUpdateDropDownList(ProductUpdateViewModel Model)
        {
            Model.Brands = await _brandRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
            Model.Categories = await _categoryRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
            Model.Tags = await _tagRepo.GetQueryable().OrderBy(a => a.Name).ToListAsync();
        }
        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            try
            {
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                ProductUpdateViewModel model = new ProductUpdateViewModel()
                {
                    Id = Product.ProductId,
                    BrandId = Product.BrandId,
                    CategoryId = Product.CategoryId,
                    TagId = Product.TagId,
                    Name = Product.Name,
                    SKU = Product.SKU,
                    Color = Product.Color,
                    Price = Product.Price,
                    Description = Product.Description,
                    OldImage = Product.Image
                };
                await ConfigureUpdateDropDownList(model);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel model, IFormFile? file)
        {
            await ConfigureUpdateDropDownList(model);
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var Product = await _productRepo.GetById(model.Id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                var ImagePath = model.OldImage;
                if (file != null)
                {
                    #region RemoveOldImage
                    var Oldpath = Path.Combine(_environment.WebRootPath, model.OldImage);
                    if ((!Directory.Exists(Oldpath)))
                    {
                        System.IO.File.Delete(Oldpath);
                    }
                    #endregion

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
                    ImagePath = $"/images/{fileName}";

                    #endregion
                }
                var Dto = new ProductUpdateDto()
                {
                    ProductId = model.Id,
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
                await _productService.Update(Dto);
                _notify.Success($"Product Updated Successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateProductSKUException ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model, IFormFile file)
        {
            await ConfigureCreateDropDownList(model);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
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
                _notify.Success($"Product created Successfully with Id - {Product.ProductId}");
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateProductSKUException ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MarkAsAvailableAsync(long id)
        {
            try
            {
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                await _productService.SetAsAvailable(id);
                _notify.Success("Product Marked as Available.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
            }
            return RedirectToAction(nameof(Index));


        }

        [HttpGet]
        public async Task<IActionResult> MarkAsUnAvailableAsync(long id)
        {
            try
            {
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                await _productService.SetAsUnAvailable(id);
                _notify.Success("Product Marked as UnAvailable.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
            }
            return RedirectToAction(nameof(Index));


        }
    }
}
