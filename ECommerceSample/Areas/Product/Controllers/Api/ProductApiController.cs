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
        private readonly BrandRepositoryInterface _brandRepo;
        private readonly TagRepositoryInterface _tagRepo;
        private readonly CategoryRepositoryInterface _categoryRepo;
        private readonly ProductServiceInterface _productService;
        private readonly ILogger<ProductApiController> _logger;


        public ProductApiController(ProductRepositoryInterface productRepo, ProductServiceInterface productService, ILogger<ProductApiController> logger, BrandRepositoryInterface brandRepo, TagRepositoryInterface tagRepo, CategoryRepositoryInterface categoryRepo)
        {
            _productRepo = productRepo;
            _productService = productService;
            _logger = logger;
            _brandRepo = brandRepo;
            _tagRepo = tagRepo;
            _categoryRepo = categoryRepo;
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
                TagId = product.TagId
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
        public async Task<IActionResult> UpdatePrice(long id,decimal price)
        {
            try
            {
                var Product = await _productRepo.GetById(id).ConfigureAwait(true) ?? throw new ProductNotFoundException();
                await _productService.UpdatePrice(id,price);
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
