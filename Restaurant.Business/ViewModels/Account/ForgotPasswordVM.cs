using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
