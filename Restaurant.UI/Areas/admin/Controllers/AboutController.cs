using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private IWebHostEnvironment _env;
        private SettingServices _settingservices;
        private string _errorMessage;

        public AboutController(AppDbContext context,
                               IMapper mapper,
                               IWebHostEnvironment env,
                               SettingServices settingServices)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
            _settingservices = settingServices;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingservices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index()
        {
            return View(_context.Abouts.FirstOrDefault());
        }
        public IActionResult Update(int id)
        {
            About dbAbout = _context.Abouts.Where(x => x.Id == id).FirstOrDefault();
            if (dbAbout is null) return NotFound();
            AboutUpdateVM about = _mapper.Map<AboutUpdateVM>(dbAbout);
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AboutUpdateVM aboutUpdate)
        {
            if (!ModelState.IsValid) return View();
            About dbAbout = _context.Abouts.Where(x => x.Id == id).FirstOrDefault();
            bool isCurrentHead = dbAbout.Head.Trim().ToLower() == aboutUpdate.Head.ToLower().Trim();
            if (!isCurrentHead)
            {
                dbAbout.Head = aboutUpdate.Head;
            }
            bool isCurrentNormalContent = dbAbout.NormalContent.Trim().ToLower() == aboutUpdate.NormalContent.ToLower().Trim();
            if (!isCurrentNormalContent)
            {
                dbAbout.NormalContent = aboutUpdate.NormalContent;
            }
            bool isCurrentItalicContent = dbAbout.ItalicContent.Trim().ToLower() == aboutUpdate.ItalicContent.ToLower().Trim();
            if (!isCurrentItalicContent)
            {
                dbAbout.ItalicContent = aboutUpdate.ItalicContent;
            }
            bool isCurrentNormalContent2 = dbAbout.NormalContent2.Trim().ToLower() == aboutUpdate.NormalContent2.ToLower().Trim();
            if (!isCurrentNormalContent2)
            {
                dbAbout.NormalContent2 = aboutUpdate.NormalContent2;
            }
            bool isCurrentLink = dbAbout.Link.Trim().ToLower() == aboutUpdate.Link.ToLower().Trim();
            if (!isCurrentLink)
            {
                dbAbout.Link = aboutUpdate.Link;
            }
            if (aboutUpdate.Photo != null)
            {
                string value=GetSetting("PhotoSize");
                int size= int.Parse(value);
                if (!CheckImageValid(aboutUpdate.Photo, "image/", size))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    return View(aboutUpdate);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbAbout.Image);
                string fileName = await Extension.SaveFileAsync(aboutUpdate.Photo, _env.WebRootPath, "assets/img");
                dbAbout.Image = fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CheckImageValid(IFormFile file, string type, int size)
        {
            if (!Extension.CheckSize(file, size))
            {
                _errorMessage = "The size of this photo is 200";
                return false;
            }
            if (!Extension.CheckType(file, type))
            {
                _errorMessage = "The type is not correct";
                return false;
            }
            return true;
        }
    }
}
