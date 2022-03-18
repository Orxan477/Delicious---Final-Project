using Microsoft.AspNetCore.Mvc;
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

        public ReservationController(AppDbContext context,
                                     SettingServices settingServices)
        {
            _context = context;
            _settingServices=settingServices;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index(int page=1)
        {
            int count = int.Parse(GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var reservs=_context.Reservations
                                .Skip((page - 1) * count)
                                .Take(count)
                                .Where(x=>!x.IsCheck && !x.IsClose)
                                .OrderByDescending(x=>x.Id).ToList();
            var reservVM = GetProductList(reservs);
            int pageCount = GetPageCount(count);
            Paginate<ReservationListVM> model = new Paginate<ReservationListVM>(reservVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.Reservations.Where(x => !x.IsCheck && !x.IsClose).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<ReservationListVM> GetProductList(List<Reservation> reservs)
        {
            List<ReservationListVM> model = new List<ReservationListVM>();
            foreach (var item in reservs)
            {
                var product = new ReservationListVM
                {
                    Id = item.Id,
                    FullName=item.FullName,
                    Email=item.Email,
                    Number=item.Number,
                    Date=item.Date, 
                    PeopleCount=item.PeopleCount,
                    Message=item.Message,
                };
                model.Add(product);
            }
            return model;
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
