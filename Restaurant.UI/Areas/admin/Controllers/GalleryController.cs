using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels.Home.Gallery;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        private IMapper _mapper;
        private SettingServices _settingServices;
        private string _errorMessage;

        public GalleryController(AppDbContext context,
                                 IWebHostEnvironment env, 
                                 IMapper mapper,
                                 SettingServices settingServices)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _settingServices = settingServices;
        }
        private int GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            string value = Settings[$"{key}"];
            return int.Parse(value);
        }
        public IActionResult Index()
        {
            ViewBag.RestaurantPhoto = _context.RestaurantPhotos.Count();
            return View(_context.RestaurantPhotos.ToList());
        }
        public IActionResult Create()
        {
            if (_context.RestaurantPhotos.Count() == 8) return BadRequest();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRestaurantPhotoVM createRestaurantPhoto)
        {
            if (!ModelState.IsValid) return View();
            int size = GetSetting("PhotoSize");
            if (!CheckImageValid(createRestaurantPhoto.Photo, "image/", size))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                return View(createRestaurantPhoto);
            }
            string fileName = await Extension.SaveFileAsync(createRestaurantPhoto.Photo, _env.WebRootPath, "assets/img");
            RestaurantPhotos restaurantPhoto = new RestaurantPhotos
            {
                Image = fileName
            };
            await _context.RestaurantPhotos.AddAsync(restaurantPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CheckImageValid(IFormFile file, string type, int size)
        {
            if (!Extension.CheckSize(file, size))
            {
                _errorMessage = $"The size of this photo is {size}";
                return false;
            }
            if (!Extension.CheckType(file, type))
            {
                _errorMessage = "The type is not correct";
                return false;
            }
            return true;
        }
        public IActionResult Update(int id)
        {
            RestaurantPhotos dbRestaurantPhoto = _context.RestaurantPhotos.Where(x => x.Id == id).FirstOrDefault();
            if (dbRestaurantPhoto is null) return NotFound();
            UpdateRestaurantPhotoVM photo = _mapper.Map<UpdateRestaurantPhotoVM>(dbRestaurantPhoto);
            return View(photo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateRestaurantPhotoVM updateRestaurantPhoto)
        {
            if (!ModelState.IsValid) return View();
            RestaurantPhotos dbRestaurantPhoto = _context.RestaurantPhotos.Where(x => x.Id == id).FirstOrDefault();
            if (updateRestaurantPhoto.Photo != null)
            {
                int size = GetSetting("PhotoSize");
                if (!CheckImageValid(updateRestaurantPhoto.Photo, "image/", size))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    return View(updateRestaurantPhoto);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbRestaurantPhoto.Image);
                string fileName = await Extension.SaveFileAsync(updateRestaurantPhoto.Photo, _env.WebRootPath, "assets/img");
                dbRestaurantPhoto.Image = fileName;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            RestaurantPhotos dbRestaurantPhoto = _context.RestaurantPhotos.Where(x => x.Id == id).FirstOrDefault();
            if (dbRestaurantPhoto is null) return NotFound();
            Helper.RemoveFile(_env.WebRootPath, "assets/img", dbRestaurantPhoto.Image);
            _context.RestaurantPhotos.Remove(dbRestaurantPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
