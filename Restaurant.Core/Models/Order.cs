namespace Restaurant.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public int FullOrderId { get; set; }
        public FullOrder FullOrder { get; set; }
        public bool IsDeleted { get; set; }
    }
}
