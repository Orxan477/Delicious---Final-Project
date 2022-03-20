using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private SettingServices _settingServices;

        public CategoryController(AppDbContext context,
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
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(_context.Categories.Where(x => !x.IsDeleted).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM categoryCreate)
        {
            if (!ModelState.IsValid) return View();
            bool name = _context.Categories.Any(x=>x.Name==categoryCreate.Name && !x.IsDeleted);
            if (name)
            {
                ModelState.AddModelError("Name", "This Product Name is available");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            Category category = _mapper.Map<Category>(categoryCreate);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Category dbCategory = _context.Categories.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbCategory is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            UpdateCategoryVM category = _mapper.Map<UpdateCategoryVM>(dbCategory);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateCategoryVM updateCategory)
        {
            if (!ModelState.IsValid) return View();
            Category dbCategory = _context.Categories.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            bool isCurrentName = dbCategory.Name.Trim().ToLower() == updateCategory.Name.ToLower().Trim();
            bool nameContext = _context.Categories.Any(x => x.Name == updateCategory.Name);
            if (nameContext && !isCurrentName)
            {
                ModelState.AddModelError("Name", "This Category is available");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            if (!isCurrentName && !nameContext)
            {
                dbCategory.Name = updateCategory.Name;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Category dbCategory = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (dbCategory is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            //_context.Categories.Remove(dbCategory);
            dbCategory.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
