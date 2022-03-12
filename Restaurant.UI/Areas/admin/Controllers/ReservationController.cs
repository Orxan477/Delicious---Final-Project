using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private AppDbContext _context;
        private SettingServices _settingServices;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View(_context.Reservations.Where(x=>!x.IsCheck && !x.IsClose).OrderByDescending(x=>x.Id).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(int id)
        {
            Reservation dbReservation = _context.Reservations.Where(x => x.Id == id && !x.IsCheck && !x.IsClose).FirstOrDefault();
            if (dbReservation is null) return NotFound();
            dbReservation.IsClose = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(int id)
        {
            Reservation dbReservation = _context.Reservations.Where(x => x.Id == id && !x.IsCheck && !x.IsClose).FirstOrDefault();
            if (dbReservation is null) return NotFound();
            dbReservation.IsCheck = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
