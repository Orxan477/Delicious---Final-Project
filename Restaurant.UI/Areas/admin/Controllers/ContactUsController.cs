using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Home.ContactUs;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        private SettingServices _settingServices;
        private AppDbContext _context;

        public ContactUsController(AppDbContext context,
                                   SettingServices settingServices)
        {
            _settingServices = settingServices;
            _context = context;
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
                                  .Skip((page - 1) * count)
                                  .Take(count)
                                  .Where(x => !x.IsDeleted)
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
                var contact = new ContactUsListVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Subject = item.Subject,
                    SentDate = item.SentDate,
                    Message = item.Message,
                };
                model.Add(contact);
            }
            return model;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            ContactUs dbContactUs = _context.ContactUs.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbContactUs is null) return NotFound();
            dbContactUs.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
