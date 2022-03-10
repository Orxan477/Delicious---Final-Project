using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels.Home.Gallery;
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
        public async Task<IActionResult> Create(CreateRestaurantPhotoVM createRestaurantPhoto)
        {
            if (!ModelState.IsValid) return View();
            if (!CheckImageValid(createRestaurantPhoto.Photo, "image/", 200))
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
        public async Task<IActionResult> Update(int id)
        {
            Team dbTeam = _context.Teams.Where(x => x.Id == id).FirstOrDefault();
            if (dbTeam is null) return NotFound();
            UpdateRestaurantPhotoVM team = _mapper.Map<UpdateTeamVM>(dbTeam);
            await GetSelectedItemAsync();
            return View(team);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateTeamVM updateTeam)
        {
            if (!ModelState.IsValid) return View();
            Team dbTeam = _context.Teams.Where(x => x.Id == id).FirstOrDefault();
            bool isCurrentName = dbTeam.FullName.Trim().ToLower() == updateTeam.FullName.ToLower().Trim();
            if (!isCurrentName)
            {
                dbTeam.FullName = updateTeam.FullName;
            }
            bool isCurrentPosition = dbTeam.PositionId == updateTeam.PositionId;
            if (!isCurrentPosition)
            {
                dbTeam.PositionId = updateTeam.PositionId;
            }
            if (updateTeam.Photo != null)
            {
                if (!CheckImageValid(updateTeam.Photo, "image/", 200))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    return View(updateTeam);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbTeam.Image);
                string fileName = await Extension.SaveFileAsync(updateTeam.Photo, _env.WebRootPath, "assets/img");
                dbTeam.Image = fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    Team dbTeam = _context.Teams.Where(x => x.Id == id).FirstOrDefault();
        //    if (dbTeam is null) return NotFound();
        //    Helper.RemoveFile(_env.WebRootPath, "assets/img", dbTeam.Image);
        //    _context.Teams.Remove(dbTeam);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
