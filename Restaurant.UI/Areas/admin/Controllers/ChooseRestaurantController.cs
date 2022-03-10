using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
   [Area("Admin")]
    public class ChooseRestaurantController : Controller
    {
        private AppDbContext _context;

        public ChooseRestaurantController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.ChooseRestaurants.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CreateMenuVM createMenu)
        //{
        //    if (!ModelState.IsValid) return View();
        //    bool isExist = _context.Products.Any(p => p.Name.Trim()
        //                                                   .ToLower() == createMenu.Name.Trim().ToLower());
        //    if (isExist)
        //    {
        //        ModelState.AddModelError("Name", "This name currently use");
        //        return View();
        //    }
        //    Product product = new Product
        //    {
        //        Name = createMenu.Name,
        //        CategoryId = createMenu.CategoryId,
        //        MenuImageId = dbImage.Id,
        //        Price = createMenu.Price,
        //        Description = createMenu.Description,

        //    };
        //    await _context.Products.AddAsync(product);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
