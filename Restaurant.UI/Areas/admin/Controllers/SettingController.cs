using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Setting;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class SettingController : Controller
    {
        private AppDbContext _context;
        private SettingServices _settingServices;
        private IMapper _mapper;

        public SettingController(AppDbContext context,
                                 SettingServices settingServices,
                                 IMapper mapper)
        {
            _context = context;
            _settingServices = settingServices;
            _mapper = mapper;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index(int page=1)
        {
            int count = int.Parse(GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var settings = _context.Settings
                                   .Skip((page - 1) * count)
                                   .Take(count)
                                   .ToList();
            var settingVM = GetProductList(settings);
            int pageCount = GetPageCount(count);
            Paginate<SettingListVM> model = new Paginate<SettingListVM>(settingVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.Settings.Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<SettingListVM> GetProductList(List<Setting> settings)
        {
            List<SettingListVM> model = new List<SettingListVM>();
            foreach (var item in settings)
            {
                SettingListVM setting = _mapper.Map<SettingListVM>(item);
                model.Add(setting);
            }
            return model;
        }
        public IActionResult Update(int id)
        {
            Setting setting = _context.Settings.Find(id);
            if (setting == null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            ViewBag.Setting = setting.Key;
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Setting setting)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(setting);
            }
            Setting dbSetting = await _context.Settings.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (dbSetting == null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            dbSetting.Value = setting.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
