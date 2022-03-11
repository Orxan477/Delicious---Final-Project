using Restaurant.Core.Models;

namespace Restaurant.Business.ViewModels.Home.Special
{
    public class CreateSpecialVM
    {
        public string InformationTabHead { get; set; }
        public string InformationTabContent { get; set; }
        public string InformationTabItalicContent { get; set; }
        public int ProductId { get; set; }
    }
}
