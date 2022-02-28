using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.DAL;

namespace Restaurant.UI.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        //private AppDbContext _context;

        //public FooterViewComponent(AppDbContext context)
        //{
        //    _context = context;
        //}
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
