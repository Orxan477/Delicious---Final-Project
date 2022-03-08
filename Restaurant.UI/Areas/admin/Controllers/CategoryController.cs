using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CategoryController(AppDbContext context,
                                  IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM categoryCreate)
        {
            if (!ModelState.IsValid) return View();
            bool name = _context.Categories.Any(x=>x.Name==categoryCreate.Name);
            if (name)
            {
                ModelState.AddModelError("Name", "This Product Name is available");
                return View();
            }
            Category category = _mapper.Map<Category>(categoryCreate);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //public async Task<IActionResult> Update(int id)
        //{
        //    Team dbTeam = _context.Teams.Where(x => x.Id == id).FirstOrDefault();
        //    if (dbTeam is null) return NotFound();
        //    UpdateTeamVM team = _mapper.Map<UpdateTeamVM>(dbTeam);
        //    await GetSelectedItemAsync();
        //    return View(team);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(int id, UpdateTeamVM updateTeam)
        //{
        //    if (!ModelState.IsValid) return View();
        //    Team dbTeam = _context.Teams.Where(x => x.Id == id).FirstOrDefault();
        //    bool isCurrentName = dbTeam.FullName.Trim().ToLower() == updateTeam.FullName.ToLower().Trim();
        //    if (!isCurrentName)
        //    {
        //        dbTeam.FullName = updateTeam.FullName;
        //    }
        //    dbTeam.Image = fileName;
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    Team dbTeam = _context.Teams.Where(x => x.Id == id).FirstOrDefault();
        //    if (dbTeam is null) return NotFound();
        //    Helper.RemoveFile(_env.WebRootPath, "assets/img", dbTeam.Image);
        //    _context.Teams.Remove(dbTeam);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
