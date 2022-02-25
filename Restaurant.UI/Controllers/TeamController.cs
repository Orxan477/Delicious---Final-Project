using Microsoft.AspNetCore.Mvc;

namespace Restaurant.UI.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
