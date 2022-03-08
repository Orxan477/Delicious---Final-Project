using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Home
{
    public class ContactUsVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
