using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Account
{
    public class ChangeEmailVM
    {
        public string NewEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
