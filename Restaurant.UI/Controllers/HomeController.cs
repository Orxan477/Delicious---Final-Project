using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Home;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                HomeIntro=await _context.HomeIntros.ToListAsync(),
                About=await _context.Abouts.FirstOrDefaultAsync(),
                AboutOptions=await _context.AboutOptions.ToListAsync(),
                Specials=await _context.Specials
                                       .Include(x=>x.MenuImage)
                                       .ToListAsync(),
                RestaurantsPhotos=await _context.RestaurantPhotos.ToListAsync(),
                Feedbacks=await _context.Feedbacks
                                        .Include(x=>x.Position)
                                        .ToListAsync(),
                ChooseRestaurants=await _context.ChooseRestaurants.ToListAsync(),
            };
            return View(homeVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(HomeVM homeVM)
        {
            if (!ModelState.IsValid) return View(homeVM);
            
            ContactUs contact = new ContactUs
            {
                Name = homeVM.ContactUsVM.Name,
                Email = homeVM.ContactUsVM.Email,
                Subject = homeVM.ContactUsVM.Subject,
                Message = homeVM.ContactUsVM.Message,
            };
            await _context.ContactUs.AddAsync(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(HomeVM homeVM)
        {
            if (!ModelState.IsValid) return View();

            Subscribe subscribe = new Subscribe
            {
                Email = homeVM.SubscribeVM.Email,
            };
            await _context.Subscribes.AddAsync(subscribe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
