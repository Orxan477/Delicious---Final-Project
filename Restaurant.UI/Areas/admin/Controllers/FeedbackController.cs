using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels.Home.Feedback;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class FeedbackController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        private IMapper _mapper;
        private SettingServices _settingServices;
        private string _errorMessage;

        public FeedbackController(AppDbContext context,
                                  IWebHostEnvironment env,
                                  IMapper mapper,
                                  SettingServices settingServices)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _settingServices = settingServices;
        }
        private int GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            string value= Settings[$"{key}"];
            return int.Parse(value);
        }
        public IActionResult Index()
        {
            return View(_context.Feedbacks.Include(x=>x.Position).ToList());
        }
        public async Task<IActionResult> Create()
        {
            await GetSelectedItemAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFeedbackVM createFeedback)
        {
            if (!ModelState.IsValid) return View();
            int size = GetSetting("PhotoSize");
            if (!CheckImageValid(createFeedback.Photo, "image/", size))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                return View(createFeedback);
            }
            string fileName = await Extension.SaveFileAsync(createFeedback.Photo, _env.WebRootPath, "assets/img");
            Feedback feedback = new Feedback
            {
                FullName = createFeedback.FullName,
                Image = fileName,
                PositionId=createFeedback.PositionId,
                Comment= createFeedback.Comment
            };
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task GetSelectedItemAsync()
        {
            ViewBag.Position = new SelectList(await _context.Positions
                                                            .ToListAsync(), "Id", "Name");
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
            Feedback dbFeedback = _context.Feedbacks.Where(x => x.Id == id).FirstOrDefault();
            if (dbFeedback is null) return NotFound();
            UpdateFeedbackVM feedback = _mapper.Map<UpdateFeedbackVM>(dbFeedback);
            await GetSelectedItemAsync();
            return View(feedback);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateFeedbackVM updateFeedback)
        {
            if (!ModelState.IsValid) return View();
            Feedback dbFeedback = _context.Feedbacks.Where(x => x.Id == id).FirstOrDefault();
            bool isCurrentName = dbFeedback.FullName.Trim().ToLower() == updateFeedback.FullName.ToLower().Trim();
            if (!isCurrentName)
            {
                dbFeedback.FullName = updateFeedback.FullName;
            }
            bool isCurrentPosition = dbFeedback.PositionId == updateFeedback.PositionId;
            if (!isCurrentPosition)
            {
                dbFeedback.PositionId = updateFeedback.PositionId;
            }
            bool isCurrentComment = dbFeedback.Comment.Trim().ToLower() == updateFeedback.Comment.Trim().ToLower();
            if (!isCurrentComment)
            {
                dbFeedback.Comment = updateFeedback.Comment;
            }
            if (updateFeedback.Photo != null)
            {
                int size = GetSetting("PhotoSize");
                if (!CheckImageValid(updateFeedback.Photo, "image/", size))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    return View(updateFeedback);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbFeedback.Image);
                string fileName = await Extension.SaveFileAsync(updateFeedback.Photo, _env.WebRootPath, "assets/img");
                dbFeedback.Image = fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Feedback dbFeedback = _context.Feedbacks.Where(x => x.Id == id).FirstOrDefault();
            if (dbFeedback is null) return NotFound();
            Helper.RemoveFile(_env.WebRootPath, "assets/img", dbFeedback.Image);
            _context.Feedbacks.Remove(dbFeedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
