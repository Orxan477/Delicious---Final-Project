using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //return View(_context.User);
            return View();
        }
    }
}
