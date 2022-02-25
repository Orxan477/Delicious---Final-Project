using Microsoft.AspNetCore.Mvc;

namespace Restaurant.UI.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
