using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Models
{
    public class ChooseRestaurant
    {
        public int Id { get; set; }
        public string CardHead { get; set; }
        public string CardContent { get; set; }
        public bool IsDeleted { get; set; }
    }
}
