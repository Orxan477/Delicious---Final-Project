using FluentValidation;
using Restaurant.Business.ViewModels.Account;

namespace Restaurant.Business.Validators.Account
{
    public class LoginVmValidator:AbstractValidator<LoginVM>
    {
        public LoginVmValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
