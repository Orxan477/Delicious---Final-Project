using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(int id)
        {
            ContactUs dbContactUs = _context.ContactUs.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbContactUs is null) return NotFound();
            dbContactUs.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
