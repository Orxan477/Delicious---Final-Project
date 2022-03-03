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
                    Products = await _context.Products.Where(x => !x.IsDeleted)
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
            Product dbProduct = await _context.Products.FindAsync(id);
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
        public async Task<IActionResult> Basket()
        {
            List<BasketVM> basket = GetBasket();
            List<BasketItemVM> model = await GetBasketList(basket);
            HomeVM homeVM = new HomeVM
            {
                BasketItemVM = model
            };
            return View(homeVM);
        }
        private async Task<List<BasketItemVM>> GetBasketList(List<BasketVM> basket)
        {
            List<BasketItemVM> model = new List<BasketItemVM>();
            foreach (BasketVM item in basket)
            {
                Product dbProduct = await _context.Products
                                            .Include(x => x.MenuImage).Include(x=>x.Category).FirstOrDefaultAsync(x => x.Id == item.Id);
                BasketItemVM basketItemVM = GetBasketItem(item, dbProduct);
                model.Add(basketItemVM);
            }
            return model;
        }
        private BasketItemVM GetBasketItem(BasketVM item, Product dbProduct)
        {
            return new BasketItemVM
            {
                Id = item.Id,
                Name = dbProduct.Name,
                Count = item.Count,
                Image = dbProduct.MenuImage.Image,
                Price = dbProduct.Price,
                Category = dbProduct.Category.Name
            };
        }
    }
}