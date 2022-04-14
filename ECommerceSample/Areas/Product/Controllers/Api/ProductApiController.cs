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
        private readonly ILogger<ProductApiController> _logger;


        public ProductApiController(ProductRepositoryInterface productRepo, ILogger<ProductApiController> logger)
        {
            _productRepo = productRepo;
            _logger = logger;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Products = await _productRepo.GetQueryable().Include(x => x.Category).Include(y => y.Brand).Include(y => y.Tag).OrderBy(a => a.ProductId).ToListAsync();
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
                var Product = await _productRepo.GetQueryable().Include(x => x.Category).Include(y => y.Brand).Include(y => y.Tag).SingleOrDefaultAsync(a=>a.ProductId==id).ConfigureAwait(true) ?? throw new ProductNotFoundException();

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
