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
    public class TagController : Controller
    {
        private readonly TagRepositoryInterface _tagRepo;
        private readonly ILogger<TagController> _logger;
        private readonly INotyfService _notify;

        public TagController(TagRepositoryInterface tagRepo, ILogger<TagController> logger, INotyfService notify)
        {
            _tagRepo = tagRepo;
            _logger = logger;
            _notify = notify;
        }

        public async Task<IActionResult> Index()
        {
            var Categories = await _tagRepo.GetQueryable().OrderBy(a => a.TagId).ToListAsync();
            var Model = new List<TagIndexViewModel>();
            foreach (var c in Categories)
            {
                Model.Add(new TagIndexViewModel()
                {
                    Id = c.TagId,
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
                var Tag = await _tagRepo.GetById(id).ConfigureAwait(true) ?? throw new TagNotFoundException();
                var Model = new TagUpdateViewModel()
                {
                    Id = Tag.TagId,
                    Name = Tag.Name
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
