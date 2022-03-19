using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Reservation;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private AppDbContext _context;
        private SettingServices _settingServices;
        private IReservationService _reservationService;

        public ReservationController(AppDbContext context,
                                     SettingServices settingServices,
                                     IReservationService reservationService)
        {
            _context = context;
            _settingServices=settingServices;
            _reservationService = reservationService;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public async Task<IActionResult> Index(int page=1)
        {
            int count = int.Parse(GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var reservs = await _reservationService.GetPaginate(count,page);
            var reservVM =  _reservationService.GetProductList( reservs);
            int pageCount = _reservationService.GetPageCount(count);
            Paginate<ReservationListVM> model = new Paginate<ReservationListVM>(reservVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,int option)
        {
            return Json(option);
            Reservation dbReservation = _context.Reservations.Where(x => x.Id == id && !x.IsCheck && !x.IsClose).FirstOrDefault();
            if (dbReservation is null) return NotFound();
            dbReservation.IsClose = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(int id,int option)
        {
            return Json(option);
            Reservation dbReservation = _context.Reservations.Where(x => x.Id == id && !x.IsCheck && !x.IsClose).FirstOrDefault();
            if (dbReservation is null) return NotFound();
            dbReservation.IsCheck = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
