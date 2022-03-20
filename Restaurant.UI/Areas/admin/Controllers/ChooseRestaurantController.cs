using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Profiles;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels.Home.Choose;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
   [Area("Admin")]
    public class ChooseRestaurantController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private SettingServices _settingServices;

        public ChooseRestaurantController(AppDbContext context,
                                          IMapper mapper,
                                          SettingServices settingServices)
        {
            _context = context;
            _mapper = mapper;
            _settingServices = settingServices;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index()
        {
            ViewBag.ChooseCount = _context.ChooseRestaurants.Where(x => !x.IsDeleted).Count();
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(_context.ChooseRestaurants.Where(x => !x.IsDeleted).ToList());
        }
        public IActionResult Create()
        {
            if (_context.ChooseRestaurants.Where(x => !x.IsDeleted).Count() == 6) return RedirectToAction("BadRequestCustom", "Error", new { area = "null" });
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChooseRestaurant chooseRestaurant)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            bool isExistHead = _context.ChooseRestaurants.Any(p => p.CardHead.Trim()
                                                           .ToLower() == chooseRestaurant.CardHead.Trim().ToLower()); 
            bool isExistContent = _context.ChooseRestaurants.Any(p => p.CardContent.Trim().
                                                           ToLower() == chooseRestaurant.CardContent.Trim().ToLower());
            if (isExistHead)
            {
                ModelState.AddModelError("CardHead", "This Head currently use");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            if (isExistContent)
            {
                ModelState.AddModelError("CardContent", "This Content currently use");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            ChooseRestaurant chooseRestaurantModel = _mapper.Map<ChooseRestaurant>(chooseRestaurant);
            await _context.ChooseRestaurants.AddAsync(chooseRestaurantModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            ChooseRestaurant dbChooseRestaurant = _context.ChooseRestaurants.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbChooseRestaurant is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            ChooseUpdateVM restaurant = _mapper.Map<ChooseUpdateVM>(dbChooseRestaurant);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(restaurant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ChooseUpdateVM chooseUpdate)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            ChooseRestaurant dbChooseRestaurant = _context.ChooseRestaurants.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            bool isCurrentName = dbChooseRestaurant.CardHead.Trim().ToLower() == chooseUpdate.CardHead.ToLower().Trim();
            bool nameContext = _context.ChooseRestaurants.Any(x => x.CardHead == chooseUpdate.CardHead);
            if (nameContext && !isCurrentName)
            {
                ModelState.AddModelError("CardHead", "This Head is available");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            if (!isCurrentName && !nameContext)
            {
                dbChooseRestaurant.CardHead = chooseUpdate.CardHead;
            }

            bool isCurrentContent = dbChooseRestaurant.CardContent.Trim().ToLower() == chooseUpdate.CardContent.ToLower().Trim();
            bool ContentContext = _context.ChooseRestaurants.Any(x => x.CardContent == chooseUpdate.CardContent);
            if (ContentContext && !isCurrentContent)
            {
                ModelState.AddModelError("CardContent", "This Content is available");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            if (!isCurrentContent && !ContentContext)
            {
                dbChooseRestaurant.CardContent = chooseUpdate.CardContent;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            ChooseRestaurant dbChooseRestaurant = _context.ChooseRestaurants.Where(x => x.Id == id).FirstOrDefault();
            if (dbChooseRestaurant is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            //_context.ChooseRestaurants.Remove(dbChooseRestaurant);
            dbChooseRestaurant.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
