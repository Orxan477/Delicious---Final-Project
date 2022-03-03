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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if(id is null) return NotFound();
            Product dbProduct = await _context.Prouducts.FindAsync(id);
            if (dbProduct is null) return BadRequest();

            List<BasketVM> basket = GetBasket();
            UpdateBasket((int)id, basket);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction("Index","Menu");
        }
        #region Check Basket
        //private async Task<ActionResult> CheckBasket(int? id)
        //{
        //    if (id == null) return RedirectToAction("Index", "Menu");
        //    Product dbProduct = await _context.Prouducts.FindAsync(id);
        //    if (dbProduct is null) return BadRequest();
        //}
        #endregion
        private void UpdateBasket(int id,List<BasketVM> basket)
        {
            BasketVM basketProduct = basket.Find(x => x.Id == id);
            if (basketProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = id,
                    Count = 1,
                    //Category = basketProduct.Category
                });
            }
            else
            {
                basketProduct.Count++;
            }
        }
        private List<BasketVM> GetBasket()
        {
            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            return basket;
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