using Restaurant.Core.Models;
using System.Collections.Generic;

namespace Restaurant.Business.ViewModels
{
    public class MenuVM
    {
        public List<Product> Products { get; set; }
        public List<Price> Price { get; set;}
    }
}
