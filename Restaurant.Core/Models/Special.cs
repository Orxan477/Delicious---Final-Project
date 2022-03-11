using Restaurant.Core.Models;

namespace Restaurant.Core.Models
{
    public class Special
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string InformationTabHead { get; set; }
        public string InformationTabContent { get; set; }
        public string InformationTabItalicContent { get; set; }
        public int MenuImageId { get; set; }
        public MenuImage MenuImage { get; set; }
    }
}
