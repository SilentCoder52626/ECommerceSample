using AspNetCoreHero.ToastNotification.Abstractions;
using ECommerce.Exceptions;
using ECommerce.Repository;
using ECommerceSample.Areas.Product.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSample.Areas.Product.Controllers
{
    [Area("Product")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly CategoryRepositoryInterface _categoryRepo;
        private readonly ILogger<CategoryController> _logger;
        private readonly INotyfService _notify;

        public CategoryController(CategoryRepositoryInterface categoryRepo, ILogger<CategoryController> logger, INotyfService notify)
        {
            _categoryRepo = categoryRepo;
            _logger = logger;
            _notify = notify;
        }

        public async Task<IActionResult> Index()
        {
            var Categories = await _categoryRepo.GetQueryable().OrderBy(a => a.CategoryId).ToListAsync();
            var Model = new List<CategoryIndexViewModel>();
            foreach (var c in Categories)
            {
                Model.Add(new CategoryIndexViewModel()
                {
                    Id = c.CategoryId,
                    Name = c.Name
                });
            }   
            return View(Model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            try
            {
                var Category = await _categoryRepo.GetById(id).ConfigureAwait(true) ?? throw new CategoryNotFoundException();
                var Model = new CategoryUpdateViewModel()
                {
                    Id = Category.CategoryId,
                    Name = Category.Name
                };
                return View(Model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
