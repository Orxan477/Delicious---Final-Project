﻿namespace Restaurant.Business.ViewModels.Menu
{
    public class BasketItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
    }
}