using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AboutOptionController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public AboutOptionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.OptionCount = _context.AboutOptions.Count();
            return View(_context.AboutOptions.ToList());
        }
        public IActionResult Create()
        {
            if (_context.AboutOptions.Count()==3) return BadRequest();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutOptionCreateVM aboutOptionCreate)
        {
            if (!ModelState.IsValid) return View();
            bool nameContext = _context.AboutOptions.Any(x => x.Option.Trim().ToLower() == aboutOptionCreate.Option.Trim().ToLower());
            if (nameContext)
            {
                ModelState.AddModelError("Option", "This Option is available");
                return View();
            }
            AboutOption aboutOption = _mapper.Map<AboutOption>(aboutOptionCreate);
            await _context.AboutOptions.AddAsync(aboutOption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            AboutOption dbAboutOption = _context.AboutOptions.Where(x => x.Id == id).FirstOrDefault();
            if (dbAboutOption is null) return NotFound();
            AboutOptionUpdateVM aboutOption = _mapper.Map<AboutOptionUpdateVM>(dbAboutOption);
            return View(aboutOption);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AboutOptionUpdateVM aboutOptionUpdate)
        {
            if (!ModelState.IsValid) return View();
            AboutOption dbAboutOption = _context.AboutOptions.Where(x => x.Id == id).FirstOrDefault();
            bool isCurrentName = dbAboutOption.Option.Trim().ToLower() == aboutOptionUpdate.Option.ToLower().Trim();
            bool nameContext = _context.AboutOptions.Any(x => x.Option.Trim().ToLower() == aboutOptionUpdate.Option.Trim().ToLower());
            if (nameContext && !isCurrentName)
            {
                ModelState.AddModelError("Option", "This Option is available");
                return View();
            }
            if (!isCurrentName && !nameContext)
            {
                dbAboutOption.Option = aboutOptionUpdate.Option;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            AboutOption dbAboutOption = _context.AboutOptions.Where(x => x.Id == id).FirstOrDefault();
            if (dbAboutOption is null) return NotFound();
            _context.AboutOptions.Remove(dbAboutOption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
