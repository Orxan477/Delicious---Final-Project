using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using System.Collections.Generic;

namespace Restaurant.UI.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private SettingServices _settingServices;

        public FooterViewComponent(SettingServices settingServices)
        {
            _settingServices = settingServices;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.Adress1 = GetSetting("Adress1");
            ViewBag.Adress2 = GetSetting("Adress2");
            ViewBag.Country = GetSetting("Country");
            ViewBag.Phone1 = GetSetting("Phone1");
            ViewBag.Email1 = GetSetting("Email1");
            return View();
        }
    }
}
