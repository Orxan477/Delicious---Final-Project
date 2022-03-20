using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Interfaces.Home;
using Restaurant.Business.Interfaces.Setting;
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
        private ISettingService _settingServices;
        private IDeliciousService _deliciousService;

        public HomeController(AppDbContext context,
                              ISettingService settingServices,
                              IDeliciousService deliciousService)
        {
            _context = context;
            _settingServices = settingServices;
            _deliciousService = deliciousService;
            
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Adress1 = _settingServices.GetSetting("Adress1");
            ViewBag.Adress2 = _settingServices.GetSetting("Adress2");
            ViewBag.ContactUsWork = _settingServices.GetSetting("ContactUsWork");
            ViewBag.WorkTime = _settingServices.GetSetting("WorkTime");
            ViewBag.Email1 = _settingServices.GetSetting("Email1");
            ViewBag.Email2 = _settingServices.GetSetting("Email2");
            ViewBag.Phone1 = _settingServices.GetSetting("Phone1");
            ViewBag.Phone2 = _settingServices.GetSetting("Phone2");
            ViewBag.RestaurantName = _settingServices.GetSetting("RestaurantName");
            HomeVM homeVM = new HomeVM
            {
                HomeIntro = await _context.HomeIntros
                                        .Where(x => !x.IsDeleted)
                                        .ToListAsync(),

                About = await _deliciousService.AboutService.GetAll(),

                AboutOptions=await _deliciousService.AboutOptionService.GetAll(),

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
                ViewBag.RestaurantName = _settingServices.GetSetting("RestaurantName");
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
                ViewBag.RestaurantName = _settingServices.GetSetting("RestaurantName");
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
