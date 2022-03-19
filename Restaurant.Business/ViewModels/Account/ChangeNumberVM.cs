using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Account
{
    public class ChangeNumberVM
    {
        public string Number { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
