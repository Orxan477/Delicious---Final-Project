using Microsoft.AspNetCore.Mvc;

namespace Restaurant.UI.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
