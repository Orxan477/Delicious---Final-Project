using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Settings.ToList());
        }
        public IActionResult Update(int id)
        {
            Setting setting = _context.Settings.Find(id);
            if (setting == null) return NotFound();
            ViewBag.Setting = setting.Key;
            return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Setting setting)
        {
            if (!ModelState.IsValid) return View(setting);
            Setting dbSetting = await _context.Settings.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (dbSetting == null) return NotFound();
            dbSetting.Value = setting.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
