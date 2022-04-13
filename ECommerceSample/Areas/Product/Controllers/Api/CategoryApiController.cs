using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Repository;
using ECommerce.Service;
using ECommerceSample.Areas.Product.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSample.Areas.Product.Controllers.Api
{
    [Route("api/product/categories")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly CategoryRepositoryInterface _categoryRepo;
        private readonly CategoryServiceInterface _categoryService;
        private readonly ILogger<CategoryApiController> _logger;
        public CategoryApiController(CategoryRepositoryInterface categoryRepo, ILogger<CategoryApiController> logger, CategoryServiceInterface categoryService)
        {
            _categoryRepo = categoryRepo;
            _logger = logger;
            _categoryService = categoryService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Categorys = await _categoryRepo.GetQueryable().OrderBy(a => a.CategoryId).ToListAsync();
                var Datas = new List<CategoryIndexViewModel>();
                foreach (var category in Categorys)
                {
                    Datas.Add(new CategoryIndexViewModel()
                    {
                        Id = category.CategoryId,
                        Name = category.Name
                    });
                }
                return Ok(Datas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var Category = await _categoryRepo.GetById(id).ConfigureAwait(true) ?? throw new CategoryNotFoundException();
                var Data = new CategoryIndexViewModel()
                {
                    Id = Category.CategoryId,
                    Name = Category.Name
                };

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var Dto = new CategoryCreateDto()
                {
                    Name = model.Name
                };
                var Category = await _categoryService.Create(Dto);
                var Data = new CategoryIndexViewModel()
                {
                    Id = Category.CategoryId,
                    Name = Category.Name
                };

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, CategoryUpdateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var Category = await _categoryRepo.GetById(id).ConfigureAwait(true) ?? throw new CategoryNotFoundException();

                var Dto = new CategoryUpdateDto()
                {
                    Name = model.Name,
                    CategoryId = id
                };
                await _categoryService.Update(Dto);
                var Data = new CategoryIndexViewModel()
                {
                    Id = Category.CategoryId,
                    Name = Category.Name
                };

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
