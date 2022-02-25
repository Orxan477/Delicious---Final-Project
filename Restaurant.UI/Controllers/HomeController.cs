using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Restaurant.UI.Controllers
{
    public class HomeController : Controller
    {
        //[Route("/#homeIntro")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
