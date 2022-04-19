using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant.Business.Services;
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
        private SettingServices _settingServices;
        private IMapper _mapper;
        private int _proCount;

        public MenuController(AppDbContext context,
                              UserManager<AppUser> userManager,
                              SettingServices settingServices,
                              IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _settingServices = settingServices;
            _mapper = mapper;
            _proCount = _context.Products.Count();
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
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
                                    .OrderByDescending(x => x.Id)
                                    .ToListAsync(),

                    Categories = await _context.Categories
                                               .Where(x => !x.IsDeleted)
                                               .ToListAsync(),
                }
            };
            ViewBag.RestaurantName = GetSetting("RestaurantName");
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
            ViewBag.RestaurantName = GetSetting("RestaurantName");
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
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return PartialView("_ModalPartial", homeVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(HomeVM homeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View();
            }

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
            if (id is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct is null) return RedirectToAction("BadRequestCustom", "Error", new { area = "null" });
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
        //    if (dbProduct is null) return RedirectToAction("BadRequestCustom", "Error", new { area = "null" });
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
                ViewBag.RestaurantName = GetSetting("RestaurantName");
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
                ViewBag.RestaurantName = GetSetting("RestaurantName");
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
                if (id == null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
                var baskets = GetBasket();
                var basket = baskets.FirstOrDefault(x => x.Id == id);
                if (basket == null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
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
                if (basket == null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
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
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(HomeVM homeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = GetSetting("RestaurantName");
                return View(homeVM.BillingAdressesVM);
            }
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.GetUserAsync(User);
            List<BasketItem> basketItems = await GetBasketProduct(user.Id);
            if (basketItems.Count == 0) return RedirectToAction("BadRequestCustom", "Error", new { area = "null" });
            await BuyProduct(basketItems,homeVM.BillingAdressesVM.Adress,user.Id);
            return RedirectToAction(nameof(MyOrder));
        }
        private async Task<List<BasketItem>> GetBasketProduct(string userId)
        {
            return await _context.BasketItems
                .Include(b => b.Product)
                .Where(b => b.AppUserId == userId)
                .ToListAsync();
        }
        private async Task BuyProduct(List<BasketItem> basketItems,string adress,string userId)
        {
            FullOrder order = new FullOrder
            {
                AppUserId=userId,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                Status="pending",
                BillingAdress = adress,
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
        }
        public async Task<IActionResult> MyOrder(int page = 1)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("BadRequestCustom", "Error", new { area = "null" });
            }
            int count = int.Parse(GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var userName = User.Identity.Name;
            var user=await _userManager.FindByNameAsync(userName);
            var FullOrders = _context.FullOrders
                                  .Where(x => x.AppUserId == user.Id)
                                 .Skip((page - 1) * count)
                                 .Take(count)
                                 .Include(x => x.Orders)
                                 .Include(x => x.AppUser)
                                 .OrderByDescending(x => x.Id)
                                 .ToList();
            //return Json(FullOrders);
            var productsVM = GetProductList(FullOrders);
            int pageCount = GetPageCount(count,user.Id);
            HomeVM homeVM = new HomeVM
            {
                Paginate = new Paginate<HomeFullOrderListVM>(productsVM, page, pageCount),
            };
            //Paginate<HomeFullOrderListVM> model = new Paginate<HomeFullOrderListVM>(productsVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(homeVM);
        }
        private int GetPageCount(int take,string userId)
        {
            var prodCount = _context.FullOrders.Where(x => x.AppUserId == userId).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<HomeFullOrderListVM> GetProductList(List<FullOrder> products)
        {
            List<HomeFullOrderListVM> model = new List<HomeFullOrderListVM>();
            foreach (var item in products)
            {
                HomeFullOrderListVM productList = _mapper.Map<HomeFullOrderListVM>(item);
                model.Add(productList);
            }
            return model;
        }
    }
}