using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;
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
            ViewBag.basketCount = _context.Products.Count();
            return View();
        }
    }
}
