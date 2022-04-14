using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Repository;
using ECommerce.Service;
using ECommerceSample.Areas.Product.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSample.Areas.Product.Controllers.Api
{
    [Route("api/product/products")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly ProductRepositoryInterface _productRepo;
        private readonly ProductServiceInterface _productService;
        private readonly ILogger<ProductApiController> _logger;


        public ProductApiController(ProductRepositoryInterface productRepo, ProductServiceInterface productService, ILogger<ProductApiController> logger, IWebHostEnvironment environment)
        {
            _productRepo = productRepo;
            _productService = productService;
            _logger = logger;
            _environment = environment;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Products = await _productRepo.GetQueryable().OrderBy(a => a.ProductId).ToListAsync();
                var Datas = new List<ProductIndexViewModel>();
                foreach (var product in Products)
                {
                    ProductIndexViewModel Model = ConfigureIndexModel(product);
                    Datas.Add(Model);
                }
                return Ok(Datas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private ProductIndexViewModel ConfigureIndexModel(ECommerce.Entity.Product product)
        {
            return new ProductIndexViewModel()
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
            };
        }

        [HttpGet("{id}/id")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                var Data = ConfigureIndexModel(Product);

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("mark-as-available/{id}")]
        public async Task<IActionResult> MarkAsAvailableAsync(long id)
        {
            try
            {
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                await _productService.SetAsAvailable(id);
                var Data = ConfigureIndexModel(Product);

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("mark-as-unavailable/{id}")]
        public async Task<IActionResult> MarkAsUnAvailableAsync(long id)
        {
            try
            {
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                await _productService.SetAsUnAvailable(id);
                var Data = ConfigureIndexModel(Product);

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-price/{id}")]
        public async Task<IActionResult> UpdatePrice(long id, decimal price)
        {
            try
            {
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                await _productService.UpdatePrice(id, price);
                var Data = ConfigureIndexModel(Product);

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, ProductUpdateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                var ImagePath = model.OldImage;
                if (model.Image != null)
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
                    string fileName = model.Image.FileName;
                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                    ImagePath = $"/images/{fileName}";

                    #endregion
                }
                var Dto = new ProductUpdateDto()
                {
                    ProductId = id,
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
                var Data = ConfigureIndexModel(Product);

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
