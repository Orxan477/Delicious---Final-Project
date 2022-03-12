using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.UI.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private AppDbContext _context;
        private SettingServices _settingServices;

        public HeaderViewComponent(AppDbContext context,
                                   SettingServices settingServices)
        {
            _context = context;
            _settingServices = settingServices;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.Phone1 = GetSetting("Phone1");
            ViewBag.NavbarWork = GetSetting("NavbarWork");
            ViewBag.WorkTime = GetSetting("WorkTime");
            ViewBag.basketCount = BasketCount();
            return View();
        }
        public int BasketCount()
        {

            if (Request.Cookies["basket"] != null)
            {
                List< BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                return basket.Count();
            }
            return 0;
        }
    }
}
