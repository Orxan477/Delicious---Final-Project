using Microsoft.AspNetCore.Http;

namespace Restaurant.Business.ViewModels.Home.About
{
    public class AboutUpdateVM
    {
        public string Head { get; set; }
        public string NormalContent { get; set; }
        public string ItalicContent { get; set; }
        public string NormalContent2 { get; set; }
        public string Link { get; set; }
        public IFormFile Photo { get; set; }
    }
}
