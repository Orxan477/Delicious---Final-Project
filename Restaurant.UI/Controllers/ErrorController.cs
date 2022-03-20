using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Interfaces.Setting;

namespace Restaurant.UI.Controllers
{
    public class ErrorController : Controller
    {
        private ISettingService _settingService;

        public ErrorController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IActionResult BadRequestCustom()
        {
            ViewBag.RestaurantName = _settingService.GetSetting("RestaurantName");
            return View();
        }
        public IActionResult NotFoundCustom()
        {
            ViewBag.RestaurantName = _settingService.GetSetting("RestaurantName");
            return View();
        }
    }
}
