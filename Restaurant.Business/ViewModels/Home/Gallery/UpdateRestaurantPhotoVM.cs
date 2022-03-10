using Microsoft.AspNetCore.Http;

namespace Restaurant.Business.ViewModels.Home.Gallery
{
    public class UpdateRestaurantPhotoVM
    {
        public IFormFile Photo { get; set; }
    }
}
