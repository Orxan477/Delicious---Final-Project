using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Controllers
{
    public class MenuController : Controller
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private int _proCount;

        public MenuController(AppDbContext context,
                              UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _proCount = _context.Products.Count();
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.productsCount = _proCount;
            HomeVM vm = new HomeVM
            {
                MenuVM = new MenuVM
                {
                    Products = await _context.Products
                                    .Where(x => !x.IsDeleted)
                                    .Include(x => x.MenuImage)
                                    .Include(x => x.Category)
                                    .OrderByDescending(p => p.Id)
                                    .ToListAsync(),

                    Categories = await _context.Categories
                                               .Where(x => !x.IsDeleted)
                                               .ToListAsync(),
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
                                .Where(x => x.Id == id)
                               .Include(x => x.MenuImage)
                               .Include(x => x.Category)
                               .ToList(),
                }

            };
            return PartialView("_ModalPartial", homeVM);
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
        public async Task<IActionResult> AddBasket(int? id, int? priceId)
        {
            if (id is null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct is null) return BadRequest();
            List<BasketVM> basket = GetBasket();

            await UpdateBasket((int)id, basket, priceId);
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
                    size = "Small";
                    break;
                case 2:
                    size = "Medium";
                    break;
                case 3:
                    size = "Large";
                    break;
                default:
                    size = "Other";
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
        private async Task UpdateBasket(int id, List<BasketVM> basket, int? priceId)
        {
            Product dbProduct = _context.Products.Find(id);
            double price = ProductPrice(dbProduct, priceId);
            string size = ProductSize(priceId);
            if (!User.Identity.IsAuthenticated)
            {
                BasketVM basketProduct = basket.Find(x => x.Id == id && x.Price == price);
                if (basketProduct is null)
                {
                    basket.Add(new BasketVM
                    {
                        Id = id,
                        Count = 1,
                        Price = price,
                        Size = size
                    });
                }
                else
                {
                    basketProduct.Count++;
                }
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            }
            else
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var dbBasket = _context.BasketItems.Where(x => x.ProductId == id && x.Price == price && x.AppUserId == user.Id).FirstOrDefault();
                var type = _context.Types.Where(x => x.Name == size).FirstOrDefault();
                if (dbBasket is null)
                {
                    BasketItem newDbBasket = new BasketItem
                    {
                        ProductId = id,
                        Count = 1,
                        Price = price,
                        TypeId = type.Id,
                        AppUserId = user.Id
                    };

                    await _context.BasketItems.AddAsync(newDbBasket);
                    await _context.SaveChangesAsync();
                    Response.Cookies.Delete("basket");
                }
                else
                {
                    dbBasket.Count++;
                    await _context.SaveChangesAsync();
                    Response.Cookies.Delete("basket");
                }
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
        private List<BasketItem> GetBasket(string userId)
        {
            var basket = _context.BasketItems.Where(x => x.AppUserId == userId).Include(x => x.Type).ToList();
            return basket;
        }
        public async Task<IActionResult> Basket()
        {

            if (!User.Identity.IsAuthenticated)
            {
                List<BasketVM> basket = GetBasket();
                List<BasketItemVM> model = await GetBasketList(basket);
                HomeVM homeVM = new HomeVM
                {
                    BasketItemVM = model
                };
                return View(homeVM);
            }
            else
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                List<BasketItem> basket = GetBasket(user.Id);
                List<BasketItemVM> model = await GetBasketList(basket);
                HomeVM homeVM = new HomeVM
                {
                    BasketItemVM = model
                };
                return View(homeVM);
            }
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
        private async Task<List<BasketItemVM>> GetBasketList(List<BasketItem> basket)
        {
            List<BasketItemVM> model = new List<BasketItemVM>();
            foreach (BasketItem item in basket)
            {
                Product dbProduct = await _context.Products
                                            .Include(x => x.MenuImage).Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == item.ProductId);
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
                Size = item.Size
            };
        }
        private BasketItemVM GetBasketItem(BasketItem item, Product dbProduct)
        {
            return new BasketItemVM
            {
                Id = item.Id,
                Name = dbProduct.Name,
                Count = item.Count,
                Image = dbProduct.MenuImage.Image,
                Price = item.Price,
                Category = dbProduct.Category.Name,
                Size = item.Type.Name
            };
        }
        [HttpPost]
        public async Task<IActionResult> EmptyCart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var basket = GetBasket(user.Id);
                foreach (var item in basket)
                {
                    _context.BasketItems.Remove(item);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                Response.Cookies.Delete("basket");
            }
            return RedirectToAction("Basket", "Menu");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int? id)
        {
            if (!User.Identity.IsAuthenticated)
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
            }
            else
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var baskets = GetBasket(user.Id);
                var basket = baskets.FirstOrDefault(x => x.Id == id);
                if (basket == null) return NotFound();
                if (basket.Count == 1 || basket.Count <= 0)
                {
                    _context.BasketItems.Remove(basket);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    basket.Count--;
                    await _context.SaveChangesAsync();
                }
            }



            return RedirectToAction("Basket", "Menu");
        }
        public IActionResult ConfirmOrder()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(HomeVM homeVM)
        {
            if (!ModelState.IsValid) return View(homeVM.BillingAdressesVM);
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.GetUserAsync(User);
            List<BasketItem> basketItems = await _context.BasketItems
                .Include(b => b.Product)
                .Where(b => b.AppUserId == user.Id)
                .ToListAsync();
            var basket = GetBasket(user.Id);
            if (basket.Count == 0) return BadRequest();
            BillingAdress billingAdress = new BillingAdress
            {
                Adress = homeVM.BillingAdressesVM.Adress,
                AppUserId = user.Id,
            };
            await _context.BillingAdresses.AddAsync(billingAdress);
            await _context.SaveChangesAsync();
            var billingId = await _context.BillingAdresses.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            FullOrder order = new FullOrder
            {
                AppUserId = user.Id,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                BillingAdressId = billingId.Id,
            };
            List<Order> orderItems = new List<Order>();

            double total = 0;

            foreach (BasketItem item in basketItems)
            {
                Order orderItem = new Order
                {
                    Count = item.Count,
                    Price = (double)item.Price,
                    ProductId = item.ProductId,
                    TypeId = item.TypeId
                };

                total += (orderItem.Count * (double)orderItem.Price);

                orderItems.Add(orderItem);
                await _context.Orders.AddAsync(orderItem);
                _context.BasketItems.Remove(item);
            }
            order.Total = total;
            order.Orders = orderItems;
            await _context.FullOrders.AddAsync(order);
            await _context.SaveChangesAsync();
            return Json("Her sey okaydir dostum");
        }
    }
}