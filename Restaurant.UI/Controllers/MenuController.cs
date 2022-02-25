using Microsoft.AspNetCore.Mvc;

namespace Restaurant.UI.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}