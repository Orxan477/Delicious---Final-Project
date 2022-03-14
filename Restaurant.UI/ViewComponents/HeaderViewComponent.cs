using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private AppDbContext _context;
        private SettingServices _settingServices;
        private UserManager<AppUser> _userManager;

        public HeaderViewComponent(AppDbContext context,
                                   SettingServices settingServices,
                                   UserManager<AppUser> userManager)
        {
            _context = context;
            _settingServices = settingServices;
            _userManager = userManager;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Phone1 = GetSetting("Phone1");
            ViewBag.NavbarWork = GetSetting("NavbarWork");
            ViewBag.WorkTime = GetSetting("WorkTime");
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.basketCount = _context.BasketItems.Where(x => x.AppUserId == user.Id).Count();
            }
            else
            {
                ViewBag.basketCount = BasketCount();
            }
            return View();
        }
        public int BasketCount()
        {

            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                return basket.Count();
            }
            return 0;
        }
    }
}
