using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Interfaces.Setting;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Reservation;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class ReservationController : Controller
    {
        private AppDbContext _context;
        private IReservationService _reservationService;

        public ReservationController(AppDbContext context,
                                     IReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }
        public IActionResult Index()
        {
            ViewBag.ReservationCountSetting = int.Parse(_reservationService.GetSetting("ReservationCount"));
            ViewBag.ReservationCountDb = _reservationService.GetAll().Result.Count();
            ViewBag.RestaurantName = _reservationService.GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReservationTable(ReservationVM reservationVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = _reservationService.GetSetting("RestaurantName");
                return View(nameof(Index));
            }
            try
            {
               await _reservationService.ReservationTable(reservationVM);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequestCustom", "Error", new { area = "null" });
            }
                return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(HomeVM homeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = _reservationService.GetSetting("RestaurantName");
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
