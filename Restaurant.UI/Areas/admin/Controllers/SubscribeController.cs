using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Footer;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private AppDbContext _context;
        private SettingServices _settingServices;
        private IMapper _mapper;

        public SubscribeController(AppDbContext context,
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
        public IActionResult Index(int page = 1)
        {
            int count = int.Parse(GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var subscribes = _context.Subscribes
                                     .Skip((page - 1) * count)
                                     .Take(count)
                                     .Where(x => !x.IsDeleted)
                                     .ToList();
            var subscribeVM = GetProductList(subscribes);
            int pageCount = GetPageCount(count);
            Paginate<SubscribeListVM> model = new Paginate<SubscribeListVM>(subscribeVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.Subscribes.Where(p => !p.IsDeleted).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<SubscribeListVM> GetProductList(List<Subscribe> subscribes)
        {
            List<SubscribeListVM> model = new List<SubscribeListVM>();
            foreach (var item in subscribes)
            {
                SubscribeListVM subscribe = _mapper.Map<SubscribeListVM>(item);
                model.Add(subscribe);
            }
            return model;
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            Subscribe dbSubscribe = _context.Subscribes.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbSubscribe is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            dbSubscribe.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
