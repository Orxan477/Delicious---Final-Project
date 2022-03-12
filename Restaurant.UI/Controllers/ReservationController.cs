using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Reservation;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class ReservationController : Controller
    {
        private AppDbContext _context;
        private SettingServices _settingServices;

        public ReservationController(AppDbContext context,
                                     SettingServices settingServices)
        {
            _context = context;
            _settingServices = settingServices;
        }
        private int GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            string value = Settings[$"{key}"];
            return int.Parse(value);
        }
        public IActionResult Index()
        {
            ViewBag.ReservationCountSetting=GetSetting("ReservationCount");
            ViewBag.ReservationCountDb = _context.Reservations.Where(x => !x.IsCheck && !x.IsClose).Count();
            return View();
        }
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReservationTable(ReservationVM reservationVM)
        {
            int ReservationCountSetting = GetSetting("ReservationCount");
            int ReservationCountDb = _context.Reservations.Where(x => !x.IsCheck && !x.IsClose).Count();
            if (ReservationCountSetting == ReservationCountDb) return BadRequest();

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
