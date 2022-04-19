using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Team;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class TeamController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private string _errorMessage;
        private IWebHostEnvironment _env;
        private SettingServices _settingServices;

        public TeamController(AppDbContext context,
                              IWebHostEnvironment env,
                              IMapper mapper,
                              SettingServices settingServices)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
            _settingServices = settingServices;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index(int page=1)
        {
            int count = int.Parse(GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var team = _context.Teams
                               .Where(x => !x.IsDeleted)
                               .Skip((page - 1) * count)
                               .Take(count)
                               .Include(x => x.Position)
                               .ToList();
            var teamVM = GetProductList(team);
            int pageCount = GetPageCount(count);
            Paginate<TeamListVM> model = new Paginate<TeamListVM>(teamVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.Teams.Where(x => !x.IsDeleted).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<TeamListVM> GetProductList(List<Team> teams)
        {
            List<TeamListVM> model = new List<TeamListVM>();
            foreach (var item in teams)
            {
                TeamListVM team=_mapper.Map<TeamListVM>(item);
                model.Add(team);
            }
            return model;
        }
        public async Task<IActionResult> Create()
        {
            await GetSelectedItemAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM teamCreate)
        {
            if (!ModelState.IsValid)
            {
                await GetSelectedItemAsync();
                return View();
            }
            int size = int.Parse(GetSetting("PhotoSize"));
            if (!CheckImageValid(teamCreate.Photo, "image/", size))
            {
                ModelState.AddModelError("Photo", _errorMessage);
               await GetSelectedItemAsync();
                return View(teamCreate);
            }
            string fileName = await Extension.SaveFileAsync(teamCreate.Photo, _env.WebRootPath, "assets/img");
            Team team = new Team
            {
                FullName = teamCreate.FullName,
                PositionId = teamCreate.PositionId,
                About=teamCreate.About,
                Image = fileName
            };
            await _context.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CheckImageValid(IFormFile file, string type, int size)
        {
            if (!Extension.CheckSize(file, size))
            {
                _errorMessage = $"The size of this photo is {size}";
                return false;
            }
            if (!Extension.CheckType(file, type))
            {
                _errorMessage = "The type is not correct";
                return false;
            }
            return true;
        }
        public async Task<IActionResult> Update(int id)
        {
            Team dbTeam = _context.Teams.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbTeam is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            UpdateTeamVM team = _mapper.Map<UpdateTeamVM>(dbTeam);
            await GetSelectedItemAsync();
            return View(team);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateTeamVM updateTeam)
        {
            if (!ModelState.IsValid)
            {
                await GetSelectedItemAsync();
                return View();
            }
            Team dbTeam = _context.Teams.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            bool isCurrentName = dbTeam.FullName.Trim().ToLower() == updateTeam.FullName.ToLower().Trim();
            if (!isCurrentName)
            {
                dbTeam.FullName = updateTeam.FullName;
            }
            bool isCurrentPosition = dbTeam.PositionId == updateTeam.PositionId;
            if (!isCurrentPosition)
            {
                dbTeam.PositionId = updateTeam.PositionId;
            }
            bool isCurrentContent = dbTeam.About.Trim().ToLower() == updateTeam.About.Trim().ToLower();
            if (!isCurrentContent)
            {
                dbTeam.About = updateTeam.About;
            }
            if (updateTeam.Photo != null)
            {
                int size = int.Parse(GetSetting("PhotoSize"));
                if (!CheckImageValid(updateTeam.Photo, "image/", size))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    await GetSelectedItemAsync();
                    return View(updateTeam);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbTeam.Image);
                string fileName = await Extension.SaveFileAsync(updateTeam.Photo, _env.WebRootPath, "assets/img");
                dbTeam.Image = fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Team dbTeam = _context.Teams.Where(x => x.Id == id).FirstOrDefault();
            if (dbTeam is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            Helper.RemoveFile(_env.WebRootPath, "assets/img", dbTeam.Image);
            //_context.Teams.Remove(dbTeam);
            dbTeam.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task GetSelectedItemAsync()
        {
            ViewBag.Position = new SelectList(await _context.Positions.Where(x => !x.IsDeleted)
                                                            .ToListAsync(), "Id", "Name");
            ViewBag.RestaurantName = GetSetting("RestaurantName");
        }
    }
}
