using ECommerce.Exceptions;
using ECommerce.Repository;
using ECommerceSample.Areas.Product.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSample.Areas.Product.Controllers.Api
{
    [Route("api/product/brands")]
    public class BrandApiController : ControllerBase
    {
        private readonly BrandRepositoryInterface _brandRepo;
        private readonly ILogger<BrandApiController> _logger;
        public BrandApiController(BrandRepositoryInterface brandRepo, ILogger<BrandApiController> logger)
        {
            _brandRepo = brandRepo;
            _logger = logger;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Brands = await _brandRepo.GetQueryable().OrderBy(a => a.BrandId).ToListAsync();
                var Datas = new List<BrandIndexViewModel>();
                foreach (var brand in Brands)
                {
                    Datas.Add(new BrandIndexViewModel()
                    {
                        Id = brand.BrandId,
                        Name = brand.Name
                    });
                }
                return Ok(Datas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}/id")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var Brand = await _brandRepo.GetById(id).ConfigureAwait(true) ?? throw new BrandNotFoundException();
                var Data = new BrandIndexViewModel()
                {
                    Id = Brand.BrandId,
                    Name = Brand.Name
                };

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
