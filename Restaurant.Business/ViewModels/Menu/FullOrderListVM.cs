using System;

namespace Restaurant.Business.ViewModels.Menu
{
    public class FullOrderListVM
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public string BillingAdress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
