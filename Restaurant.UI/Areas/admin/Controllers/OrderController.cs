using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
