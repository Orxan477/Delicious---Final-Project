using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Home.ContactUs;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class ContactUsController : Controller
    {
        private SettingServices _settingServices;
        private AppDbContext _context;
        private IMapper _mapper;
        private IWebHostEnvironment _env;
        private IConfiguration _configure;

        public ContactUsController(AppDbContext context,
                                   SettingServices settingServices,
                                   IMapper mapper,
                                   IWebHostEnvironment env,
                                   IConfiguration configure)
        {
            _settingServices = settingServices;
            _context = context;
            _mapper = mapper;
            _env = env;
            _configure = configure;
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
            var contact = _context.ContactUs
                                  .Where(x => !x.IsDeleted)
                                  .Skip((page - 1) * count)
                                  .Take(count)
                                  .OrderByDescending(x => x.Id).ToList();
            var contactVM = GetProductList(contact);
            int pageCount = GetPageCount(count);
            Paginate<ContactUsListVM> model = new Paginate<ContactUsListVM>(contactVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.ContactUs.Where(p => !p.IsDeleted).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<ContactUsListVM> GetProductList(List<ContactUs> contacts)
        {
            List<ContactUsListVM> model = new List<ContactUsListVM>();
            foreach (var item in contacts)
            {
                ContactUsListVM contact = _mapper.Map<ContactUsListVM>(item);
                model.Add(contact);
            }
            return model;
        }
        public IActionResult SendMessage(int id)
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            ContactUs dbContactUs = _context.ContactUs.Where(p => p.Id == id && !p.IsDeleted).FirstOrDefault();
            ContactUsListVM model = new ContactUsListVM
            {
                Id = id,
                Message = dbContactUs.Message,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactUsListVM contactUs)
        {
            if (ModelState["SendMessage"].ValidationState == ModelValidationState.Invalid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(contactUs);
            }
            ContactUs dbContactUs = _context.ContactUs.Where(x => x.Id == contactUs.Id && !x.IsDeleted).FirstOrDefault();
            if (dbContactUs is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            await SendEmailAnswer(contactUs.SendMessage, dbContactUs);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return RedirectToAction(nameof(Index));
        }
        private async Task SendEmailAnswer(string message, ContactUs dbContactUs)
        {
            string name = GetSetting("RestaurantName");
            string body = string.Empty;
            using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "assets", "SendMessage", "ContactUs.html")))
            {
                body = streamReader.ReadToEnd();
            }
            body = body.Replace("{{send}}", $"{message}").Replace("{{restaurantName}}", $"{name}");
        TryAgain:
            try
            {
                Email.SendEmail(_configure.GetSection("Email:SenderEmail").Value,
                       _configure.GetSection("Email:Password").Value, dbContactUs.Email, body, $"{name} - Contact");
            }
            catch (Exception ex)
            {
                goto TryAgain;
            }

            dbContactUs.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            ContactUs dbContactUs = await _context.ContactUs.Where(x => !x.IsDeleted).FirstOrDefaultAsync();
            if (dbContactUs == null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            dbContactUs.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
