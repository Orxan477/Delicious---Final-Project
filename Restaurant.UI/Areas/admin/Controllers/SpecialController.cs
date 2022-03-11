using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.ViewModels.Home.Special;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class SpecialController : Controller
    {
        private AppDbContext _context;

        public SpecialController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.SpecialCount=_context.Specials.Count();
            return View(_context.Specials.Include(x => x.MenuImage).ToList());
        }
        public async Task<IActionResult> Create()
        {
            await GetSelectedItemAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSpecialVM createSpecial)
        {
            if (!ModelState.IsValid) return View();
            bool isExistFoodName=_context.Specials.Any(x=>x.FoodName.Trim().ToLower()==
                                                                    createSpecial.FoodName.ToLower());
            if (isExistFoodName)
            {
                ModelState.AddModelError("FoodName", "Information about this product is available");
                return View(createSpecial);
            }
            Product dbProduct = await _context.Products.Where(x => !x.IsDeleted && x.Id == createSpecial.ProductId).Include(x=>x.MenuImage).FirstOrDefaultAsync();
            Special special = new Special
            {
                FoodName = dbProduct.Name,
                PropHead = createSpecial.PropHead,
                PropContent = createSpecial.PropContent,
                PropContentItalic = createSpecial.PropContentItalic,
                MenuImageId = dbProduct.MenuImage.Id
            };
            await _context.Specials.AddAsync(special);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task GetSelectedItemAsync()
        {
            ViewBag.Product = new SelectList(await _context.Products
                                                            .ToListAsync(), "Id", "Name");
        }
    }
}
