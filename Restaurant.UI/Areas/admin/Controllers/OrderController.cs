using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.Services;
using Restaurant.Business.ViewModels;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private SettingServices _settingServices;
        private IMapper _mapper;

        public OrderController(AppDbContext context,
                               UserManager<AppUser> userManager,
                               SettingServices settingServices,
                               IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _settingServices = settingServices;
            _mapper = mapper;
        }
        private string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _settingServices.GetSetting();
            return Settings[$"{key}"];
        }
        public IActionResult Index(int page=1)
        {
            //var user = await _userManager.GetUserAsync(User);
            int count = int.Parse(GetSetting("TakeCount"));
            ViewBag.TakeCount = count;
            var fullOrders =  _context.FullOrders
                                       .Skip((page - 1) * count)
                                       .Take(count)
                                       .Include(x => x.Orders)
                                       .Include(x => x.AppUser)
                                       .OrderByDescending(x=>x.Id)
                                       .ToList();
            var orderVM = GetProductList(fullOrders);
            int pageCount = GetPageCount(count);
            Paginate<FullOrderListVM> model = new Paginate<FullOrderListVM>(orderVM, page, pageCount);
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var prodCount = _context.FullOrders.Where(x=>!x.IsDeleted).Count();
            return (int)Math.Ceiling((decimal)prodCount / take);
        }
        private List<FullOrderListVM> GetProductList(List<FullOrder> feedbacks)
        {
            List<FullOrderListVM> model = new List<FullOrderListVM>();
            foreach (var item in feedbacks)
            {
                FullOrderListVM order = _mapper.Map<FullOrderListVM>(item);
                model.Add(order);
            }
            return model;
        }
        public IActionResult Detail(int id)
        {
            ViewBag.RestaurantName = GetSetting("RestaurantName");
            return View(_context.FullOrders
                                         .Include(x => x.Orders)
                                         .ThenInclude(x=>x.Type)
                                         .Include(x => x.Orders)
                                         .ThenInclude(x=>x.Product)
                                         .ThenInclude(x=>x.MenuImage)
                                         .Include(x => x.AppUser)
                                         .FirstOrDefault(x => x.Id == id));
        }
    }
}
