using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels.Position;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private SettingServices _settingServices;

        public PositionController(AppDbContext context,
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
            return View(_context.Positions.Where(x => !x.IsDeleted).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePositionVM createPosition)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            bool name = _context.Categories.Any(x => x.Name == createPosition.Name);
            if (name)
            {
                ModelState.AddModelError("Name", "This Position Name is available");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            Position position = _mapper.Map<Position>(createPosition);
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Position dbPosition = _context.Positions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbPosition is null) return NotFound();
            UpdatePositionVM position = _mapper.Map<UpdatePositionVM>(dbPosition);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(position);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdatePositionVM updatePosition)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            Position dbPosition = _context.Positions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            bool isCurrentName = dbPosition.Name.Trim().ToLower() == updatePosition.Name.ToLower().Trim();
            bool nameContext = _context.Categories.Any(x => x.Name == updatePosition.Name);
            if (nameContext && !isCurrentName)
            {
                ModelState.AddModelError("Name", "This Category is available");
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            if (!isCurrentName && !nameContext)
            {
                dbPosition.Name = updatePosition.Name;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Position dbPosition = _context.Positions.Where(x => x.Id == id).FirstOrDefault();
            if (dbPosition is null) return NotFound();
            //_context.Positions.Remove(dbPosition);
            dbPosition.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

