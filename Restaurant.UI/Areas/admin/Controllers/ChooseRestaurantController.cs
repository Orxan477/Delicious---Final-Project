using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Profiles;
using Restaurant.Business.ViewModels.Home.Choose;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
   [Area("Admin")]
    public class ChooseRestaurantController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public ChooseRestaurantController(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.ChooseCount = _context.ChooseRestaurants.Where(x => !x.IsDeleted).Count();
            return View(_context.ChooseRestaurants.Where(x => !x.IsDeleted).ToList());
        }
        public IActionResult Create()
        {
            if (_context.ChooseRestaurants.Where(x => !x.IsDeleted).Count() == 6) return BadRequest();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChooseRestaurant chooseRestaurant)
        {
            if (!ModelState.IsValid) return View();
            bool isExistHead = _context.ChooseRestaurants.Any(p => p.CardHead.Trim()
                                                           .ToLower() == chooseRestaurant.CardHead.Trim().ToLower()); 
            bool isExistContent = _context.ChooseRestaurants.Any(p => p.CardContent.Trim().
                                                           ToLower() == chooseRestaurant.CardContent.Trim().ToLower());
            if (isExistHead)
            {
                ModelState.AddModelError("CardHead", "This Head currently use");
                return View();
            }
            if (isExistContent)
            {
                ModelState.AddModelError("CardContent", "This Content currently use");
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
            if (dbChooseRestaurant is null) return NotFound();
            ChooseUpdateVM restaurant = _mapper.Map<ChooseUpdateVM>(dbChooseRestaurant);
            return View(restaurant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ChooseUpdateVM chooseUpdate)
        {
            if (!ModelState.IsValid) return View();
            ChooseRestaurant dbChooseRestaurant = _context.ChooseRestaurants.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            bool isCurrentName = dbChooseRestaurant.CardHead.Trim().ToLower() == chooseUpdate.CardHead.ToLower().Trim();
            bool nameContext = _context.ChooseRestaurants.Any(x => x.CardHead == chooseUpdate.CardHead);
            if (nameContext && !isCurrentName)
            {
                ModelState.AddModelError("CardHead", "This Head is available");
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
            if (dbChooseRestaurant is null) return NotFound();
            //_context.ChooseRestaurants.Remove(dbChooseRestaurant);
            dbChooseRestaurant.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
