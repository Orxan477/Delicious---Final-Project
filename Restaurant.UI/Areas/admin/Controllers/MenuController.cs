using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using Restaurant.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        private IMapper _mapper;
        private SettingServices _settingServices;
        private PaginateRepository<Product,ProductListVM> _repository;
        private string _errorMessage;

        public MenuController(AppDbContext context,
                              IWebHostEnvironment env,
                              IMapper mapper,
                              SettingServices settingServices,
                              PaginateRepository<Product,ProductListVM> repository)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _settingServices = settingServices;
            _repository = repository;
        }
        private int GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            string value = Settings[$"{key}"];
            return int.Parse(value);
        }
        public async Task<IActionResult> Index(int page=1)
        {
            int count= GetSetting("TakeCount");
            ViewBag.TakeCount = count;
           //var products=_context.Products
           //                     .Skip((page - 1) * count)
           //                     .Take(count)
           //                     .Include(x => x.MenuImage)
           //                     .Include(x => x.Category)
           //                     .ToList();
         List<Product>products1= await  _repository.GetPaginate(count, page, "MenuImage,Category");
            return Json(products1);

            //var productsVM = GetProductList(products);
            //int pageCount = GetPageCount(count);
            //Paginate<ProductListVM> model = new Paginate<ProductListVM>(productsVM, page, pageCount);
            //return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.Products.Where(p => !p.IsDeleted).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<ProductListVM> GetProductList(List<Product> products)
        {
            List<ProductListVM> model = new List<ProductListVM>();
            foreach (var item in products)
            {
                ProductListVM productList=_mapper.Map<ProductListVM>(item);
                //var product = new ProductListVM
                //{
                //    Id = item.Id,
                //    Name = item.Name,
                //    Price = item.Price,
                //    Description=item.Description,
                //    Category = item.Category.Name,
                //    Image = item.MenuImage.Image,
                //};
                model.Add(productList);
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
        public async Task<IActionResult> Create(CreateMenuVM createMenu)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.Products.Any(p => p.Name.Trim()
                                                           .ToLower() == createMenu.Name.Trim().ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This name currently use");
                return View();
            }
            int size = GetSetting("PhotoSize");
            if (!CheckImageValid(createMenu.Photo, "image/", size))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                return View(createMenu);
            }
            string fileName = await Extension.SaveFileAsync(createMenu.Photo, _env.WebRootPath, "assets/img");
            MenuImage image = new MenuImage
            {
                Image = fileName,
            };
            await _context.MenuImages.AddAsync(image);
            await _context.SaveChangesAsync();
            MenuImage dbImage = await _context.MenuImages.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            Product product = new Product
            {
                Name = createMenu.Name,
                CategoryId = createMenu.CategoryId,
                MenuImageId = dbImage.Id,
                Price = createMenu.Price,
                Description = createMenu.Description,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            Product dbProduct = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if (dbProduct is null) return NotFound();
            UpdateMenuVM product = _mapper.Map<UpdateMenuVM>(dbProduct);
            await GetSelectedItemAsync();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateMenuVM updateMenu)
        {
            if (!ModelState.IsValid) return View();
            Product dbProduct = _context.Products.Where(x => x.Id == id).Include(x => x.MenuImage).FirstOrDefault();
            bool isCurrentName = dbProduct.Name.Trim().ToLower() == updateMenu.Name.ToLower().Trim();
            if (!isCurrentName)
            {
                dbProduct.Name = updateMenu.Name;
            }
            bool isCurrentCategory = dbProduct.CategoryId == updateMenu.CategoryId;
            if (!isCurrentCategory)
            {
                dbProduct.CategoryId = updateMenu.CategoryId;
            }
            bool isCurrentPrice = dbProduct.Price == updateMenu.Price;
            if (!isCurrentPrice)
            {
                dbProduct.Price = updateMenu.Price;
            }
            if (updateMenu.Photo != null)
            {
                int size = GetSetting("PhotoSize");
                if (!CheckImageValid(updateMenu.Photo, "image/", size))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    return View(updateMenu);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbProduct.MenuImage.Image);
                string fileName = await Extension.SaveFileAsync(updateMenu.Photo, _env.WebRootPath, "assets/img");
                MenuImage image = new MenuImage
                {
                    Image = fileName,
                };
                await _context.MenuImages.AddAsync(image);
                await _context.SaveChangesAsync();
                MenuImage dbImage = await _context.MenuImages.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                dbProduct.MenuImageId = dbImage.Id;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Product dbProduct = _context.Products.Where(x => x.Id == id).Include(x => x.MenuImage).FirstOrDefault();
            if (dbProduct is null) return NotFound();
            MenuImage dbImage = await _context.MenuImages.Where(x => x.Id == dbProduct.MenuImageId).FirstOrDefaultAsync();
            Helper.RemoveFile(_env.WebRootPath, "assets/img", dbProduct.MenuImage.Image);
            _context.Products.Remove(dbProduct);
            _context.MenuImages.Remove(dbImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task GetSelectedItemAsync()
        {
            ViewBag.Category = new SelectList(await _context.Categories
                                                            .ToListAsync(), "Id", "Name");
        }
    }
}
