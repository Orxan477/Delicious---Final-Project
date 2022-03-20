using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Interfaces;
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
        private IReservationService _reservationService;
        private IMapper _mapper;

        public ReservationController(AppDbContext context,
                                     SettingServices settingServices,
                                     IReservationService reservationService,
                                     IMapper mapper)
        {
            _context = context;
            _settingServices = settingServices;
            _reservationService = reservationService;
            _mapper = mapper;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index()
        {
            ViewBag.ReservationCountSetting=int.Parse(GetSetting("ReservationCount"));
            ViewBag.ReservationCountDb = _reservationService.GetAll().Result.Count();
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReservationTable(ReservationVM reservationVM)
        {
            int ReservationCountSetting = int.Parse(GetSetting("ReservationCount"));
            int ReservationCountDb = _context.Reservations.Where(x => !x.IsCheck && !x.IsClose).Count();
            if (ReservationCountSetting == ReservationCountDb) return BadRequest();
            if (reservationVM.PeopleCount > 10) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(nameof(Index));
            }
            Reservation reservation=_mapper.Map<Reservation>(reservationVM);
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(HomeVM homeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }

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
