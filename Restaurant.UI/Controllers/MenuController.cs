using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class MenuController : Controller
    {
        private AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM
            {
                MenuVM = new MenuVM
                {
                    Products = await _context.Prouducts.Where(x => !x.IsDeleted)
                                    .Include(x => x.MenuImage)
                                    .Include(x => x.Category)
                                    .OrderByDescending(p => p.Id)
                                    .ToListAsync(),
                    Categories = await _context.Categories.ToListAsync(),
                }
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(HomeVM homeVM)
        {
            if (!ModelState.IsValid) return View();

            Subscribe subscribe = new Subscribe
            {
                Email = homeVM.SubscribeVM.Email,
            };
            await _context.Subscribes.AddAsync(subscribe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if(id is null) return NotFound();
            Product dbProduct = await _context.Prouducts.FindAsync(id);
            if (dbProduct is null) return BadRequest();
            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket= new List<BasketVM>();
            }
            BasketVM basketProduct = basket.Find(x => x.Id == dbProduct.Id);
            if (basketProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                basketProduct.Count++;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction("Index","Menu");
        }
        public IActionResult Basket()
        {
            //List<BasketVM> basket;
            //if (Request.Cookies["basket"] != null)
            //{
            //    basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            //}
            //else
            //{
            //    basket = new List<BasketVM>();
            //}
            return Json(JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]));
        }
    }
}