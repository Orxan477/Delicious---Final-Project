using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Menu
{
    public class BillingAdressVM
    {
        [Required,MaxLength(70)]
        public string Adress { get; set; }
        public string AppUserId { get; set; }
    }
}
