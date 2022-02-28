using System.Collections.Generic;

namespace Restaurant.Core.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Content { get; set; }
        public IList<ProductPrice> ProductPrices { get; set; }
    }
}
