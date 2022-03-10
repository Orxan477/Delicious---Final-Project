using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Utilities;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        private string _errorMessage;

        public GalleryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.RestaurantPhoto=_context.RestaurantPhotos.Count();
            return View(_context.RestaurantPhotos.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM teamCreate)
        {
            if (!ModelState.IsValid) return View();
            if (!CheckImageValid(teamCreate.Photo, "image/", 200))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                return View(teamCreate);
            }
            string fileName = await Extension.SaveFileAsync(teamCreate.Photo, _env.WebRootPath, "assets/img");
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
