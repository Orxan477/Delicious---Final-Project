using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Business.ViewModels.Menu;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.UI.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            
            ViewBag.basketCount = BasketCount();
            return View();
        }
        public int BasketCount()
        {

            if (Request.Cookies["basket"] != null)
            {
                List< BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                return basket.Count();
            }
            return 0;
        }
    }
}
