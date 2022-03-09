using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Abouts.FirstOrDefault());
        }
        public IActionResult Update(int id)
        {
            About dbAbout = _context.Abouts.Where(x => x.Id == id).FirstOrDefault();
            if (dbAbout is null) return NotFound();
            HomeIntroUpdateVM homeIntro = _mapper.Map<HomeIntroUpdateVM>(dbAbout);
            return View(homeIntro);
        }
    }
}
