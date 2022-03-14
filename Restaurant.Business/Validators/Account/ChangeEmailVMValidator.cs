using FluentValidation;
using Restaurant.Business.ViewModels.Account;

namespace Restaurant.Business.Validators.Account
{
    public class ChangeEmailVMValidator:AbstractValidator<ChangeEmailVM>
    {
        public ChangeEmailVMValidator()
        {
            RuleFor(x => x.NewEmail).NotNull().NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
