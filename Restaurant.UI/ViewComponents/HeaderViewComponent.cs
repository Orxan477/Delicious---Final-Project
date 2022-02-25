using Microsoft.AspNetCore.Mvc;

namespace Restaurant.UI.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
