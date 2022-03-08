using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;
using System.Linq;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        private AppDbContext _context;

        public ContactUsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.ContactUs.Where(x=>!x.IsDeleted).OrderByDescending(x=>x.Id).ToList());
        }
    }
}
