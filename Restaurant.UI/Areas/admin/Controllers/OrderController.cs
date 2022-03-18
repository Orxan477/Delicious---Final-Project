using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private SettingServices _settingServices;

        public OrderController(AppDbContext context,
                               UserManager<AppUser> userManager,
                               SettingServices settingServices)
        {
            _context = context;
            _userManager = userManager;
            _settingServices = settingServices;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public async Task<IActionResult> Index()
        {
            //var user = await _userManager.GetUserAsync(User);

            List<FullOrder> fullOrders = await _context.FullOrders
                                                .Include(x => x.Orders)
                                                .Include(x => x.BillingAdress).ThenInclude(x=>x.AppUser)
                                                .OrderByDescending(x=>x.Id)
                                                .ToListAsync();
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(fullOrders);
        }
        public IActionResult Detail(int id)
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(_context.FullOrders
                                         .Include(x => x.Orders)
                                         .ThenInclude(x=>x.Type)
                                         .Include(x => x.Orders)
                                         .ThenInclude(x=>x.Product)
                                         .ThenInclude(x=>x.MenuImage)
                                         .Include(x => x.BillingAdress)
                                         .ThenInclude(x => x.AppUser)
                                         .FirstOrDefault(x => x.Id == id));
        }
    }
}
