using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Users.OrderByDescending(x=>x.Id).ToList());
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser dbUser = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (dbUser is null) return NotFound();
            if (!dbUser.IsDeleted)
            {

                dbUser.IsDeleted = true;
            }
            else
            {
                dbUser.IsDeleted = false;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
