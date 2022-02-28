namespace Restaurant.Core.Models
{
    public class ProductPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PriceId { get; set; }
        public Price Price { get; set; }
    }
}
