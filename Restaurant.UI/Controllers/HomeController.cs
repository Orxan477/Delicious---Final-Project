﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Home;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                HomeIntro=await _context.HomeIntros.ToListAsync(),
                About=await _context.Abouts.FirstOrDefaultAsync(),
                AboutOptions=await _context.AboutOptions.ToListAsync(),
                Specials=await _context.Specials
                                       .Include(x=>x.MenuImage)
                                       .ToListAsync(),
                RestaurantsPhotos=await _context.RestaurantPhotos.ToListAsync(),
                Feedbacks=await _context.Feedbacks
                                        .Include(x=>x.Position)
                                        .ToListAsync(),
                ChooseRestaurants=await _context.ChooseRestaurants.ToListAsync(),
            };
            return View(homeVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsVM contactVM)
        {
            if(!ModelState.IsValid) return View(contactVM);

            ContactUs contact = new ContactUs
            {
                Name = contactVM.Name,
                Email = contactVM.Email,
                Subject = contactVM.Subject,
                Message = contactVM.Message,
            };
            await _context.ContactUs.AddAsync(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
