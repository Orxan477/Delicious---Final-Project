using FluentValidation;
using Restaurant.Business.ViewModels.Account;

namespace Restaurant.Business.Validators.Account
{
    public class ChangeNumberVMValidation:AbstractValidator<ChangeNumberVM>
    {
        public ChangeNumberVMValidation()
        {
            RuleFor(x => x.Number).NotEmpty().NotNull().MaximumLength(14);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
