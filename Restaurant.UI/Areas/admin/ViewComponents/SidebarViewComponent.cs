using Microsoft.AspNetCore.Mvc;

namespace Restaurant.UI.Areas.admin.ViewComponents
{
    public class SidebarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
