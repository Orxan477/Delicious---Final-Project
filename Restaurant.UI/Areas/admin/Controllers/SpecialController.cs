using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels.Home.Special;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class SpecialController : Controller
    {
        private IMapper _mapper;
        private AppDbContext _context;
        private SettingServices _settingServices;

        public SpecialController(AppDbContext context,
                                 IMapper mapper,
                                 SettingServices settingServices)
        {
            _mapper = mapper;
            _context = context;
            _settingServices = settingServices;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index()
        {
            ViewBag.SpecialCount=_context.Specials.Where(x => !x.IsDeleted).Count();
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(_context.Specials.Where(x => !x.IsDeleted).Include(x => x.MenuImage).ToList());
        }
        public async Task<IActionResult> Create()
        {
            if (_context.Specials.Where(x => !x.IsDeleted).Count() == 5)
            {
                return RedirectToAction("BadRequestCustom", "Error", new { area = "null" });
            }
            await GetSelectedItemAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateSpecialVM createSpecial)
        {
            if (!ModelState.IsValid)
            {
                await GetSelectedItemAsync();
                return View();
            }
            
            Product dbProduct = await _context.Products.Where(x => !x.IsDeleted && x.Id == createSpecial.ProductId).Include(x=>x.MenuImage).FirstOrDefaultAsync();
            if (dbProduct is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            bool isExistFoodName=_context.Specials.Any(x=>x.FoodName.Trim().ToLower()==
                                                                    dbProduct.Name.ToLower().Trim());
            if (isExistFoodName)
            {
                ModelState.AddModelError("FoodName", "Information about this product is available");
                await GetSelectedItemAsync();
                return View(createSpecial);
            }
            Special special = new Special
            {
                FoodName = dbProduct.Name,
                InformationTabHead = createSpecial.InformationTabHead,
                InformationTabContent = createSpecial.InformationTabContent,
                InformationTabItalicContent = createSpecial.InformationTabItalicContent,
                MenuImageId = dbProduct.MenuImage.Id
            };
            await _context.Specials.AddAsync(special);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Special dbSpecial = _context.Specials.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (dbSpecial is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            CreateUpdateSpecialVM position = _mapper.Map<CreateUpdateSpecialVM>(dbSpecial);
            await GetSelectedItemAsync();
            return View(position);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CreateUpdateSpecialVM updateSpecialVM)
        {
            if (!ModelState.IsValid)
            {
                await GetSelectedItemAsync();
                return View();
            }
            Special dbSpecial = _context.Specials.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            Product dbProduct = await _context.Products.Where(x => !x.IsDeleted && x.Id == updateSpecialVM.ProductId)
                                                                            .Include(x => x.MenuImage).FirstOrDefaultAsync();
            if (dbProduct is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            bool isExistFoodNameContext = _context.Specials.Any(x => x.FoodName.Trim().ToLower() ==
                                                                             dbProduct.Name.ToLower().Trim());

            bool isExistFoodName = dbSpecial.FoodName.Trim().ToLower() == dbProduct.Name.Trim().ToLower();
            if (isExistFoodNameContext && !isExistFoodName)
            {
                ModelState.AddModelError("ProductId", "Information about this product is available");
                await GetSelectedItemAsync();
                return View(updateSpecialVM);
            }
            
            bool isCurrentTabHead = dbSpecial.InformationTabHead.Trim().ToLower() ==
                                                                updateSpecialVM.InformationTabHead.Trim().ToLower();
            if(!isCurrentTabHead) dbSpecial.InformationTabHead = updateSpecialVM.InformationTabHead;

            if (updateSpecialVM.InformationTabContent != null)
            {
                bool isCurrentTabContent = dbSpecial.InformationTabContent.Trim().ToLower() ==
                                                               updateSpecialVM.InformationTabContent.Trim().ToLower();
                if (!isCurrentTabContent) dbSpecial.InformationTabContent = updateSpecialVM.InformationTabContent;
            }

            if (updateSpecialVM.InformationTabItalicContent != null)
            {
                if (dbSpecial.InformationTabItalicContent ==null)
                {
                    dbSpecial.InformationTabItalicContent = updateSpecialVM.InformationTabItalicContent;
                }
                bool isCurrentTabItalicContent = dbSpecial.InformationTabItalicContent.Trim().ToLower() ==
                                                               updateSpecialVM.InformationTabItalicContent.Trim().ToLower();
                if (!isCurrentTabItalicContent) dbSpecial.InformationTabItalicContent = updateSpecialVM.InformationTabItalicContent;
            }

            bool isCurrentProductId = dbSpecial.MenuImageId == dbProduct.MenuImage.Id;
            if (!isCurrentProductId) dbSpecial.MenuImageId = dbProduct.MenuImage.Id;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Special dbSpecial = _context.Specials.Where(x => x.Id == id).FirstOrDefault();
            if (dbSpecial is null) return RedirectToAction("NotFoundCustom", "Error", new { area = "null" });
            //_context.Specials.Remove(dbSpecial);
            dbSpecial.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task GetSelectedItemAsync()
        {
            ViewBag.Product = new SelectList(await _context.Products
                                                            .ToListAsync(), "Id", "Name");
            ViewBag.RestaurantName = GetSetting("RestaurantName");
        }
    }
}
