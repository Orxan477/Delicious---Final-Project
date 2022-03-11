using Restaurant.Core.Models;

namespace Restaurant.Business.ViewModels.Home.Special
{
    public class CreateSpecialVM
    {
        public string FoodName { get; set; }
        public string PropHead { get; set; }
        public string PropContent { get; set; }
        public string PropContentItalic { get; set; }
        public int ProductId { get; set; }
    }
}
