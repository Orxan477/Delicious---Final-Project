using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Account
{
    public class ChangeUsernameVM
    {
        public string NewUsername { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
