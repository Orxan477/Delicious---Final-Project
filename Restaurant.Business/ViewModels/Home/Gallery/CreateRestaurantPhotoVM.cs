using Microsoft.AspNetCore.Http;

namespace Restaurant.Business.ViewModels.Home.Gallery
{
    public class CreateRestaurantPhotoVM
    {
        public IFormFile Photo { get; set; }
    }
}
