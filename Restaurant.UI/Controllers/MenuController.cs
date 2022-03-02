using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class MenuController : Controller
    {
        private AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM
            {
                MenuVM = new MenuVM
                {
                    Products = await _context.Prouducts.Where(x => !x.IsDeleted)
                                    .Include(x => x.MenuImage)
                                    .Include(x => x.Category)
                                    .OrderByDescending(p => p.Id)
                                    .ToListAsync(),
                    Categories = await _context.Categories.ToListAsync(),
                }
            };
            return View(vm);
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

        public IActionResult AddBasket(int? id)
        {
            if(id is null) return NotFound();
            return Json(id);
        }
    }
}