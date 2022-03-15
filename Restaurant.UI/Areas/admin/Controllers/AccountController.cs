using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.ViewModels.Account;
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
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IMapper _mapper;

        public AccountController(AppDbContext context,
                                 UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(_context.Users.OrderByDescending(x => x.Id).ToList());
        }
        public async Task<IActionResult> Update(string id)
        {
            if (id == null) return NotFound();
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null) return NotFound();
            var userRole = await _userManager.GetRolesAsync(appUser);
            AppUserVM appUserVM = new AppUserVM
            {
                Email = appUser.Email,
                FullName = appUser.FullName,
                Role = userRole.FirstOrDefault(),
            };
            await GetSelectedItemAsync();
            return View(appUserVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, AppUserVM appUserVM)
        {
            if (ModelState["NewRole"].ValidationState == ModelValidationState.Invalid)
            {
                await GetSelectedItemAsync();
                return View(appUserVM);
            }

            AppUser user = await _userManager.FindByIdAsync(id);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);

            }
            await _userManager.AddToRoleAsync(user, appUserVM.NewRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private async Task GetSelectedItemAsync()
        {
            ViewBag.Roles = new SelectList(await _context.Roles
                                                            .ToListAsync(), "Name");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser dbUser = await _userManager.FindByIdAsync(id);
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
