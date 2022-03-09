using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels.Home.HomeIntro;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
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

        public HomeIntroController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.HomeIntros.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeIntroCreateVM homeIntroCreate)
        {
            if (!ModelState.IsValid) return View();
            if (!CheckImageValid(homeIntroCreate.Photo, "image/", 200))
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
        public IActionResult Update(int id)
        {
            HomeIntro dbHomeIntro = _context.HomeIntros.Where(x => x.Id == id).FirstOrDefault();
            if (dbHomeIntro is null) return NotFound();
            HomeIntroUpdateVM homeIntro = _mapper.Map<HomeIntroUpdateVM>(dbHomeIntro);
            return View(homeIntro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, HomeIntroUpdateVM homeIntroUpdate)
        {
            if (!ModelState.IsValid) return View();
            HomeIntro dbHomeIntro = _context.HomeIntros.Where(x => x.Id == id).FirstOrDefault();
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
                if (!CheckImageValid(homeIntroUpdate.Photo, "image/", 200))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    return View(homeIntroUpdate);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbHomeIntro.Image);
                string fileName = await Extension.SaveFileAsync(homeIntroUpdate.Photo, _env.WebRootPath, "assets/img");
                dbHomeIntro.Image = fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
