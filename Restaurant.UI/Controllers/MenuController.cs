using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
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
            //List<Product> products = _context.Prouducts.Where(x => !x.IsDeleted)
            //                    .Include(x => x.MenuImage)
            //                    .Include(x => x.Category)
            //                    .ToList();
            MenuVM vm = new MenuVM
            {
                Products = await _context.Prouducts.Where(x => !x.IsDeleted)
                                    .Include(x => x.MenuImage)
                                    .Include(x => x.Category)
                                    .Include(x=>x.ProductPrices)
                                    .ToListAsync(),
            };
            return View(vm);
        }
    }
}