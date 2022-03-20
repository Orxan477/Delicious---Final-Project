using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Reservation;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class ReservationController : Controller
    {
        private IDeliciousService _deliciousService;

        public ReservationController(IDeliciousService deliciousService)
        {
            _deliciousService = deliciousService;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            int count = int.Parse(_deliciousService.ReservationService.GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var reservs = await _deliciousService.ReservationService.GetPaginate(count,page);
            var reservVM = _deliciousService.ReservationService.GetProductList( reservs);
            int pageCount = _deliciousService.ReservationService.GetPageCount(count);
            Paginate<ReservationListVM> model = new Paginate<ReservationListVM>(reservVM, page, pageCount);
            ViewBag.RestaurantName = _deliciousService.ReservationService.GetSetting("RestaurantName");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,int option)
        {
            var a=await _deliciousService.ReservationService.Update(id,option);
            return RedirectToAction(nameof(Index));
        }
    }
}
