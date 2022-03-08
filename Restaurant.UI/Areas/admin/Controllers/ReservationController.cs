using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;
using System.Linq;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Reservations.Where(x=>!x.IsDeleted).ToList());
        }
    }
}
