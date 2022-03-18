using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Account;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
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
        private SettingServices _settingServices;

        public AccountController(AppDbContext context,
                                 UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IMapper mapper,
                                 SettingServices settingService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _settingServices = settingService;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index(int page = 1)
        {
            int count = int.Parse(GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var users = _context.Users.OrderByDescending(x => x.Id).ToList();
            var userVM = GetProductList(users);
            int pageCount = GetPageCount(count);
            Paginate<UserListVM> model = new Paginate<UserListVM>(userVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.Users.Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<UserListVM> GetProductList(List<AppUser> users)
        {
            List<UserListVM> model = new List<UserListVM>();
            foreach (var item in users)
            {
                UserListVM user=_mapper.Map<UserListVM>(item);
                model.Add(user);
            }
            return model;
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
            ViewBag.RestaurantName = GetSetting("RestaurantName");
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
