using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Menu
{
    public class CreateMenuVM
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Photo { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
