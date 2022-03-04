using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.DAL;

namespace Restaurant.UI.Areas.admin.Controllers
{
    public class HomeIntroController : Controller
    {
        private AppDbContext _context;

        public HomeIntroController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.HomeIntros.ToListAsync());
        }
    }
}
