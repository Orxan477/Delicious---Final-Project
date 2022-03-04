using Microsoft.AspNetCore.Mvc;

namespace Restaurant.UI.Areas.admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
