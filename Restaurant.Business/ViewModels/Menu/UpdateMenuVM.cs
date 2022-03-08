using Microsoft.AspNetCore.Http;

namespace Restaurant.Business.ViewModels.Menu
{
    public class UpdateMenuVM
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Photo { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
