using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;
using System.Linq;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM teamCreate)
        {
            if (!ModelState.IsValid) return View();
            Team team = new Team
            {
                FullName = teamCreate.FullName,
                PositionId = teamCreate.PositionId,
                Image = fileName
            };
            await _context.AddAsync(team);
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
