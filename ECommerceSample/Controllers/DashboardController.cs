using ECommerce.Repository;
using ECommerceSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSample.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ProductRepositoryInterface _productRepo;
        private readonly BrandRepositoryInterface _brandRepo;
        private readonly CategoryRepositoryInterface _categoryRepo;
        private readonly TagRepositoryInterface _tagRepo;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ProductRepositoryInterface productRepo, BrandRepositoryInterface brandRepo,
            CategoryRepositoryInterface categoryRepo, TagRepositoryInterface tagRepo, ILogger<DashboardController> logger)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
            _tagRepo = tagRepo;
            _logger = logger;
        }

        public async Task<IActionResult> Index(DashboardViewModel model)
        {
            model.TotalProducts = await _productRepo.GetQueryable().CountAsync();
            model.TotalCategories = await _categoryRepo.GetQueryable().CountAsync();
            model.TotalBrands = await _brandRepo.GetQueryable().CountAsync();
            model.TotalTags = await _tagRepo.GetQueryable().CountAsync();
            return View(model);
        }
    }
}
