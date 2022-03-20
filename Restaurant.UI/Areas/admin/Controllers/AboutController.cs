using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Interfaces.Home;
using Restaurant.Business.ViewModels.Home.About;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private IAboutService _aboutService;
        private readonly IDeliciousService _deliciousService;

        public AboutController(IAboutService aboutService,
                               IDeliciousService deliciousService)
        {
            _aboutService = aboutService;
            _deliciousService = deliciousService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.RestaurantName = _aboutService.GetSetting("RestaurantName");
            return View(await _aboutService.GetAll());
        }
        public async Task<IActionResult> Update(int id)
        {
            AboutUpdateVM about = await _deliciousService.AboutService.GetMap(id);
            ViewBag.RestaurantName = _aboutService.GetSetting("RestaurantName");
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AboutUpdateVM aboutUpdate)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = _aboutService.GetSetting("RestaurantName");
                return View(aboutUpdate);
            }
            try
            {
                await _aboutService.Update(id, aboutUpdate);
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("Photo", ex.Message);
                ViewBag.RestaurantName = _aboutService.GetSetting("RestaurantName");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
