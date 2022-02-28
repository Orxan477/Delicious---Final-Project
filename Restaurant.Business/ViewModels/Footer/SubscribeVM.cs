using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Footer
{
    public class SubscribeVM
    {
        public int Id { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
