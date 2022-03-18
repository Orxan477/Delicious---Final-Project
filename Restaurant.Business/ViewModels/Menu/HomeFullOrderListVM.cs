using System;

namespace Restaurant.Business.ViewModels.Menu
{
    public class HomeFullOrderListVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}
