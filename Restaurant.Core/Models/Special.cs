using Restaurant.Core.Models;

namespace Restaurant.Core.Models
{
    public class Special
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string PropHead { get; set; }
        public string PropContent { get; set; }
        public string PropContentItalic { get; set; }
        public MenuImage MenuImage { get; set; }
    }
}
