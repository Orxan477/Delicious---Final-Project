using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class TeamController : Controller
    {
        private AppDbContext _context;
        private SettingServices _settingServices;

        public TeamController(AppDbContext context,
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
        public IActionResult Index()
        {
            HomeVM vm = new HomeVM
            {
                Teams = _context.Teams
                                .Where(x => !x.IsDeleted)
                                .Include(x=>x.Position)
                                .ToList(),
            };
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(HomeVM homeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }

            Subscribe subscribe = new Subscribe
            {
                Email = homeVM.SubscribeVM.Email,
            };
            await _context.Subscribes.AddAsync(subscribe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
