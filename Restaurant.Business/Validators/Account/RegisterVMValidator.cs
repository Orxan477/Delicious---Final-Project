using FluentValidation;
using Restaurant.Business.ViewModels.Account;

namespace Restaurant.Business.Validators.Account
{
    public class RegisterVMValidator:AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {
            RuleFor(x=>x.FullName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x=>x.UserName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(x=>x.Number).NotEmpty().NotNull().MaximumLength(14);
            RuleFor(x=>x.Password).NotEmpty().NotNull().MaximumLength(255);
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().Equal(x => x.Password)
                                                                            .WithMessage("Password is not equal");
        }
    }
}
