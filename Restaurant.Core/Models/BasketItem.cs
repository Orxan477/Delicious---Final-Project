﻿namespace Restaurant.Core.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
