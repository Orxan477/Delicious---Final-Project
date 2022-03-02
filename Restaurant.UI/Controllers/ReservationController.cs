using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Reservation;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class ReservationController : Controller
    {
        private AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReservationTable(ReservationVM reservationVM)
        {
            if (!ModelState.IsValid) return View(nameof(Index));

            Reservation reservation = new Reservation
            {
                FullName = reservationVM.FullName,
                Email = reservationVM.Email,
                Number = reservationVM.Number,
                Date = reservationVM.Date,
                PeopleCount = reservationVM.PeopleCount,
                Message = reservationVM.Message,

            };
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    }
}
