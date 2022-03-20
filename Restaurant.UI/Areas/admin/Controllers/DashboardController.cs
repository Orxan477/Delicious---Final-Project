using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Interfaces.Setting;
using Restaurant.Data.DAL;
using System.Linq;

namespace Restaurant.UI.Areas.admin.Controllers
{
        [Area("Admin")]
    public class DashboardController : Controller
    {
        private AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ReservationCount = _context.Reservations.ToList().Count();
            ViewBag.FullOrderCount=_context.FullOrders.ToList().Count(); 
            ViewBag.UserCount= _context.Users.ToList().Count();
            ViewBag.TeamCount= _context.Teams.ToList().Count();
            ViewBag.ContactUsCount=_context.ContactUs.ToList().Count();
            ViewBag.SubscribeCount = _context.Subscribes.ToList().Count;
            return View();
        }
    }
}
