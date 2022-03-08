using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Menu
{
    public class CreateMenuVM
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
