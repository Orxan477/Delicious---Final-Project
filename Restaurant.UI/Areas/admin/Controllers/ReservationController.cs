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
        private IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            int count = int.Parse(_reservationService.GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var reservs = await _reservationService.GetPaginate(count,page);
            var reservVM =  _reservationService.GetProductList( reservs);
            int pageCount = _reservationService.GetPageCount(count);
            Paginate<ReservationListVM> model = new Paginate<ReservationListVM>(reservVM, page, pageCount);
            ViewBag.RestaurantName = _reservationService.GetSetting("RestaurantName");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,int option)
        {
            var a=await _reservationService.Update(id,option);
            return RedirectToAction(nameof(Index));
        }
    }
}
