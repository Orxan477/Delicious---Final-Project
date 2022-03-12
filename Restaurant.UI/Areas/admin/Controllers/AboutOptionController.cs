using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AboutOptionController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private SettingServices _settingServices;

        public AboutOptionController(AppDbContext context,
                                     IMapper mapper,
                                     SettingServices settingServices)
        {
            _context = context;
            _mapper = mapper;
            _settingServices = settingServices;
        }
        private int GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            string value = Settings[$"{key}"];
            return int.Parse(value);
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.OptionCount = _context.AboutOptions
                                          .Where(x => !x.IsDeleted)
                                          .Count();
            int count = GetSetting("TakeCount");
            ViewBag.TakeCount = count;
            var option = _context.AboutOptions
                                .Where(x => !x.IsDeleted)
                                .Skip((page - 1) * count)
                                .Take(count)
                                .ToList();
            var optionVm = GetProductList(option);
            int pageCount = GetPageCount(count);
            Paginate<AboutOptionListVM> model = new Paginate<AboutOptionListVM>(optionVm, page, pageCount);
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.AboutOptions.Where(x => !x.IsDeleted).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<AboutOptionListVM> GetProductList(List<AboutOption> options)
        {
            List<AboutOptionListVM> model = new List<AboutOptionListVM>();
            foreach (var item in options)
            {
                var option = new AboutOptionListVM
                {
                    Id = item.Id,
                    Option=item.Option,
                };
                model.Add(option);
            }
            return model;
        }
        public IActionResult Create()
        {
            if (_context.AboutOptions.Where(x => !x.IsDeleted).Count()==3) return BadRequest();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutOptionCreateVM aboutOptionCreate)
        {
            if (!ModelState.IsValid) return View();
            bool nameContext = _context.AboutOptions.Any(x => x.Option.Trim().ToLower() == aboutOptionCreate.Option.Trim().ToLower());
            if (nameContext)
            {
                ModelState.AddModelError("Option", "This Option is available");
                return View();
            }
            AboutOption aboutOption = _mapper.Map<AboutOption>(aboutOptionCreate);
            await _context.AboutOptions.AddAsync(aboutOption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            AboutOption dbAboutOption = _context.AboutOptions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbAboutOption is null) return NotFound();
            AboutOptionUpdateVM aboutOption = _mapper.Map<AboutOptionUpdateVM>(dbAboutOption);
            return View(aboutOption);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AboutOptionUpdateVM aboutOptionUpdate)
        {
            if (!ModelState.IsValid) return View();
            AboutOption dbAboutOption = _context.AboutOptions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            bool isCurrentName = dbAboutOption.Option.Trim().ToLower() == aboutOptionUpdate.Option.ToLower().Trim();
            bool nameContext = _context.AboutOptions.Any(x => x.Option.Trim().ToLower() == aboutOptionUpdate.Option.Trim().ToLower());
            if (nameContext && !isCurrentName)
            {
                ModelState.AddModelError("Option", "This Option is available");
                return View();
            }
            if (!isCurrentName && !nameContext)
            {
                dbAboutOption.Option = aboutOptionUpdate.Option;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            AboutOption dbAboutOption = _context.AboutOptions.Where(x => x.Id == id).FirstOrDefault();
            if (dbAboutOption is null) return NotFound();
            //_context.AboutOptions.Remove(dbAboutOption);
            dbAboutOption.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
