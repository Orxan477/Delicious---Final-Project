using Microsoft.AspNetCore.Http;

namespace Restaurant.Business.ViewModels.Home.HomeIntro
{
    public class HomeIntroCreateVM
    {
        public string Head { get; set; }
        public string Content { get; set; }
        public IFormFile Photo { get; set; }
    }
}
