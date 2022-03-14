using FluentValidation;
using Restaurant.Business.ViewModels.Account;

namespace Restaurant.Business.Validators.Account
{
    public class ChangeUsernameVMValdiator:AbstractValidator<ChangeUsernameVM>
    {
        public ChangeUsernameVMValdiator()
        {
            RuleFor(x => x.NewUsername).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
