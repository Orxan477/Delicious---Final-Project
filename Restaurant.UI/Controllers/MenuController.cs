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
        private int _proCount;

        public MenuController(AppDbContext context)
        {
            _context = context;
            _proCount = _context.Products.Count();
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.productsCount = _proCount;
            HomeVM vm = new HomeVM
            {
                MenuVM = new MenuVM
                {
                    Products = await _context.Products.Where(x => !x.IsDeleted)
                                    .Include(x => x.MenuImage)
                                    .Include(x => x.Category)
                                    .OrderByDescending(p => p.Id)
                                    //.Take(2)
                                    .ToListAsync(),
                    Categories = await _context.Categories.ToListAsync(),
                }
            };
            return View(vm);
        }
        public IActionResult LoadProduct(int skip)
        {
            if (_proCount == skip)
            {
                return Json(new
                {
                    alter = "No Product"
                });
            }
            HomeVM homeVM = new HomeVM
            {
                MenuVM = new MenuVM
                {

                    Products = _context.Products
                                .OrderByDescending(x => x.Id)
                                .Skip(skip)
                                .Take(2)
                                .Include(x => x.MenuImage)
                                .Include(x => x.Category)
                                .ToList(),
                }

            };

            return PartialView("_MenuPartial", homeVM);

        }
        public IActionResult ModalPartial(int id)
        {
            HomeVM homeVM = new HomeVM
            {
                MenuVM = new MenuVM
                {

                    Products = _context.Products
                                .Where(x=>x.Id==id)
                               .Include(x => x.MenuImage)
                               .Include(x => x.Category)
                               .ToList(),
                }

            };
            return PartialView("_ModalPartial",homeVM);
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id,int? priceId)
        {
            if (id is null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct is null) return BadRequest();

            List<BasketVM> basket = GetBasket();
            UpdateBasket((int)id, basket,priceId);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction("Index", "Menu");
        }
        private double ProductPrice(Product dbProduct, int? priceId)
        {
            double price = 1;
            
            if (priceId != null)
            {
                switch (priceId)
                {
                    case 1:
                        price = (double)dbProduct.Price;
                        break;
                    case 2:
                        price = (double)(dbProduct.Price * 1.5);
                        break;
                    case 3:
                        price = (double)(dbProduct.Price * 2.5);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                price = (int)(dbProduct.Price);
            }
            return price;
        }
        private string ProductSize(int? priceId)
        {
            string size = string.Empty;
            switch (priceId)
            {
                case 1:
                    size = "Kiçik";
                    break;
                case 2:
                    size = "Orta";
                    break;
                case 3:
                    size = "Böyük";
                    break;
                default:
                    break;
            }
            return size;

        }
        #region Check Basket
        //private async Task<ActionResult> CheckBasket(int? id)
        //{
        //    if (id == null) return RedirectToAction("Index", "Menu");
        //    Product dbProduct = await _context.Prouducts.FindAsync(id);
        //    if (dbProduct is null) return BadRequest();
        //}
        #endregion
        private void UpdateBasket(int id, List<BasketVM> basket,int?priceId)
        {
            Product dbProduct =  _context.Products.Find(id);
            double price=ProductPrice(dbProduct, priceId);
            string size=ProductSize(priceId);
            BasketVM basketProduct = basket.Find(x => x.Id == id && x.Price==price);
            if (basketProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = id,
                    Count = 1,
                    Price=price,
                    Size= size
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
                                            .Include(x => x.MenuImage).Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == item.Id);
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
                Price = item.Price,
                Category = dbProduct.Category.Name,
                Size=item.Size
            };
        }
        [HttpPost]
        public IActionResult EmptyCart()
        {
            Response.Cookies.Delete("basket");
            return RedirectToAction("Basket", "Menu");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int? id)
        {
            if (id == null) return NotFound();
            var baskets = GetBasket();
            var basket = baskets.FirstOrDefault(x => x.Id == id);
            if (basket == null) return NotFound();
            if (basket.Count == 1 || basket.Count <= 0)
            {
                baskets.Remove(basket);
            }
            else
            {
                basket.Count--;
            }


            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));
            return RedirectToAction("Basket", "Menu");
        }
    }
}