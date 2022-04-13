using ECommerce.Exceptions;
using ECommerce.Repository;
using ECommerceSample.Areas.Product.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Dto;
using ECommerce.Service;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceSample.Areas.Product.Controllers
{
    [Area("Product")]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly BrandRepositoryInterface _brandRepo;
        private readonly BrandServiceInterface _brandService;
        private readonly ILogger<BrandController> _logger;
        private readonly INotyfService _notify;
        public BrandController(BrandRepositoryInterface brandRepo, ILogger<BrandController> logger, BrandServiceInterface brandService, INotyfService notyf)
        {
            _brandRepo = brandRepo;
            _logger = logger;
            _brandService = brandService;
            _notify = notyf;
        }

        public async Task<IActionResult> Index()
        {
            var Datas = new List<BrandIndexViewModel>();
            var Brands = await _brandRepo.GetQueryable().OrderBy(a=>a.BrandId).ToListAsync();
            foreach (var Brand in Brands)
            {
                Datas.Add(new BrandIndexViewModel()
                {
                    Id = Brand.BrandId,
                    Name = Brand.Name
                });
            }
            return View(Datas);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                var BrandCreateDto = new BrandCreateDto()
                {
                    Name = vm.Name
                };
                var Brand = await _brandService.Create(BrandCreateDto);
                _notify.Success($"Brand ({Brand.Name}) Created Successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch(DuplicateBrandNameException ex)
            {
                ModelState.AddModelError(nameof(BrandCreateViewModel.Name),ex.Message);
                _logger.LogError(ex,ex.Message);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return View(vm);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            try
            {
                var Brand= await _brandRepo.GetById(id).ConfigureAwait(true)??throw new BrandNotFoundException();
                var model = new BrandUpdateViewModel()
                {
                    Id = id,
                    Name = Brand.Name,
                };
                return View(model);
            }catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Update(BrandUpdateViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                var BrandUpdateDto = new BrandUpdateDto()
                {
                    Name = vm.Name,
                    BrandId = vm.Id
                };
                await _brandService.Update(BrandUpdateDto);
                _notify.Success($"Brand Updatad Successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateBrandNameException ex)
            {
                ModelState.AddModelError(nameof(BrandCreateViewModel.Name), ex.Message);
                _logger.LogError(ex, ex.Message);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _notify.Error(ex.Message);
                return View(vm);
            }
        }
    }
}
