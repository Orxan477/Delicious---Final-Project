using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;
using System.Linq;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private AppDbContext _context;

        public SubscribeController(AppDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View(_context.Subscribes.Where(x=>x.);
        //}
    }
}
