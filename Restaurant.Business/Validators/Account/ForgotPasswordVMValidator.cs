using FluentValidation;
using Restaurant.Business.ViewModels.Account;

namespace Restaurant.Business.Validators.Account
{
    public class ForgotPasswordVMValidator:AbstractValidator<ForgotPasswordVM>
    {
        public ForgotPasswordVMValidator()
        {
            RuleFor(x => x.NewPassword).NotEmpty().NotNull().MaximumLength(255);
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().Equal(x => x.NewPassword)
                                                                            .WithMessage("Password is not equal");
        }
    }
}
