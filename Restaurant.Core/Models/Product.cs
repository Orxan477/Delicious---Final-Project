using Restaurant.Core.Models;
using System.Collections.Generic;

namespace Restaurant.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ProductPrice> ProductPrices { get; set; }
        public string Description { get; set; }
        public MenuImage MenuImage { get; set; }
        public Category Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
