using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Index()
        {
            return View(_context.Subscribes.Where(x => !x.IsDeleted).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Subscribe dbSubscribe = _context.Subscribes.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbSubscribe is null) return NotFound();
            dbSubscribe.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
