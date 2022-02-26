using Restaurant.Core.Models;

namespace Restaurant.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public MenuImage MenuImage { get; set; }
        public Category Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
