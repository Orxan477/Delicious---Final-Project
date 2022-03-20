using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Interfaces.Home;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Home;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        private SettingServices _settingServices;
        private IDeliciousService _deliciousService;

        public HomeController(AppDbContext context,
                              SettingServices settingServices,
                              IDeliciousService deliciousService)
        {
            _context = context;
            _settingServices = settingServices;
            _deliciousService = deliciousService;
            
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Adress1 = GetSetting("Adress1");
            ViewBag.Adress2 = GetSetting("Adress2");
            ViewBag.ContactUsWork = GetSetting("ContactUsWork");
            ViewBag.WorkTime = GetSetting("WorkTime");
            ViewBag.Email1 = GetSetting("Email1");
            ViewBag.Email2 = GetSetting("Email2");
            ViewBag.Phone1 = GetSetting("Phone1");
            ViewBag.Phone2 = GetSetting("Phone2");
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            HomeVM homeVM = new HomeVM
            {
                HomeIntro = await _context.HomeIntros
                                        .Where(x => !x.IsDeleted)
                                        .ToListAsync(),

                About = await _deliciousService.AboutService.GetAll(),

                AboutOptions=await _context.AboutOptions
                                           .Where(x => !x.IsDeleted)
                                           .ToListAsync(),

                Specials=await _context.Specials
                                       .Where(x => !x.IsDeleted)
                                       .Include(x=>x.MenuImage)
                                       .ToListAsync(),

                RestaurantsPhotos=await _context.RestaurantPhotos.ToListAsync(),

                Feedbacks=await _context.Feedbacks
                                        .Where(x => !x.IsDeleted)
                                        .Include(x=>x.Position)
                                        .ToListAsync(),

                ChooseRestaurants=await _context.ChooseRestaurants
                                                .Where(x => !x.IsDeleted)
                                                .ToListAsync(),
            };
            return View(homeVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(HomeVM homeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(homeVM);
            }

            ContactUs contact = new ContactUs
            {
                Name = homeVM.ContactUsVM.Name,
                Email = homeVM.ContactUsVM.Email,
                Subject = homeVM.ContactUsVM.Subject,
                Message = homeVM.ContactUsVM.Message,
                SentDate = DateTime.Now,
            };
            await _context.ContactUs.AddAsync(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
