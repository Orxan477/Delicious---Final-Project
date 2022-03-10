using Microsoft.AspNetCore.Http;

namespace Restaurant.Business.ViewModels.Home.Gallery
{
    internal class UpdateRestauranPhotoVM
    {
        public IFormFile Photo { get; set; }
    }
}
