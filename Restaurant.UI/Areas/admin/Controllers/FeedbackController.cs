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
using Restaurant.Business.ViewModels.Home.Feedback;
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
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index(int page =1)
        {
            int count = int.Parse(GetSetting("TakeCount")); 
            ViewBag.TakeCount = count;
            var feedbacks = _context.Feedbacks
                                .Skip((page - 1) * count)
                                .Take(count)
                                .Include(x => x.Position)
                                .ToList();
            var feedbackVM = GetProductList(feedbacks);
            int pageCount = GetPageCount(count);
            Paginate<FeedbackListVM> model = new Paginate<FeedbackListVM>(feedbackVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.Feedbacks.Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<FeedbackListVM> GetProductList(List<Feedback> feedbacks)
        {
            List<FeedbackListVM> model = new List<FeedbackListVM>();
            foreach (var item in feedbacks)
            {
                FeedbackListVM feedback=_mapper.Map<FeedbackListVM>(item);
                model.Add(feedback);
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
        public async Task<IActionResult> Create(CreateFeedbackVM createFeedback)
        {
            if (!ModelState.IsValid)
            {
               await GetSelectedItemAsync();
                return View();
            }
            int size = int.Parse(GetSetting("PhotoSize"));
            if (!CheckImageValid(createFeedback.Photo, "image/", size))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                await GetSelectedItemAsync();
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
            ViewBag.RestaurantName = GetSetting("RestaurantName");
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
            if (dbFeedback is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            UpdateFeedbackVM feedback = _mapper.Map<UpdateFeedbackVM>(dbFeedback);
            await GetSelectedItemAsync();
            //ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(feedback);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateFeedbackVM updateFeedback)
        {
            if (!ModelState.IsValid)
            {
                await GetSelectedItemAsync();
                return View();
            }
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
                int size = int.Parse(GetSetting("PhotoSize"));
                if (!CheckImageValid(updateFeedback.Photo, "image/", size))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    await GetSelectedItemAsync();
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
            if (dbFeedback is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            Helper.RemoveFile(_env.WebRootPath, "assets/img", dbFeedback.Image);
            _context.Feedbacks.Remove(dbFeedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
