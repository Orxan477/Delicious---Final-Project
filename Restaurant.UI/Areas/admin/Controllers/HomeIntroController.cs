using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Home.HomeIntro;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class HomeIntroController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private string _errorMessage;
        private IWebHostEnvironment _env;
        private SettingServices _settingServices;

        public HomeIntroController(AppDbContext context,
                                   IMapper mapper,
                                   IWebHostEnvironment env,
                                   SettingServices settingServices)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
            _settingServices = settingServices;
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
            var intro = _context.HomeIntros
                                .Where(x => !x.IsDeleted)
                                .Skip((page - 1) * count)
                                .Take(count)
                                .ToList();
            var introVM = GetProductList(intro);
            int pageCount = GetPageCount(count);
            Paginate<IntroListVM> model = new Paginate<IntroListVM>(introVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.HomeIntros.Where(x => !x.IsDeleted).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<IntroListVM> GetProductList(List<HomeIntro> intros)
        {
            List<IntroListVM> model = new List<IntroListVM>();
            foreach (var item in intros)
            {
                IntroListVM intro = _mapper.Map<IntroListVM>(item);
                model.Add(intro);
            }
            return model;
        }
        public IActionResult Create()
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeIntroCreateVM homeIntroCreate)
        {
            if (!ModelState.IsValid) return View();
            int size = int.Parse(GetSetting("PhotoSize"));
            if (!CheckImageValid(homeIntroCreate.Photo, "image/", size))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                return View(homeIntroCreate);
            }
            string fileName = await Extension.SaveFileAsync(homeIntroCreate.Photo, _env.WebRootPath, "assets/img");
            HomeIntro homeIntro = new HomeIntro
            {
                Head = homeIntroCreate.Head,
                Content = homeIntroCreate.Content,
                Image = fileName
            };
            await _context.HomeIntros.AddAsync(homeIntro);
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
            HomeIntro dbHomeIntro = _context.HomeIntros.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbHomeIntro is null) return NotFound();
            HomeIntroUpdateVM homeIntro = _mapper.Map<HomeIntroUpdateVM>(dbHomeIntro);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(homeIntro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, HomeIntroUpdateVM homeIntroUpdate)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }
            HomeIntro dbHomeIntro = _context.HomeIntros.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            bool isCurrentHead = dbHomeIntro.Head.Trim().ToLower() == homeIntroUpdate.Head.ToLower().Trim();
            if (!isCurrentHead)
            {
                dbHomeIntro.Head = homeIntroUpdate.Head;
            }
            bool isCurrentContent = dbHomeIntro.Content.Trim().ToLower() == homeIntroUpdate.Content.ToLower().Trim();
            if (!isCurrentContent)
            {
                dbHomeIntro.Content = homeIntroUpdate.Content;
            }
            if (homeIntroUpdate.Photo != null)
            {
                int size = int.Parse(GetSetting("PhotoSize"));
                if (!CheckImageValid(homeIntroUpdate.Photo, "image/", size))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    ViewBag.RestaurantName = GetSetting("RestaurantName");
                    return View(homeIntroUpdate);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbHomeIntro.Image);
                string fileName = await Extension.SaveFileAsync(homeIntroUpdate.Photo, _env.WebRootPath, "assets/img");
                dbHomeIntro.Image = fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            HomeIntro dbHomeIntro = _context.HomeIntros.Where(x => x.Id == id).FirstOrDefault();
            if (dbHomeIntro is null) return NotFound();
            Helper.RemoveFile(_env.WebRootPath, "assets/img", dbHomeIntro.Image);
            //_context.HomeIntros.Remove(dbHomeIntro);
            dbHomeIntro.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
