using FluentValidation;
using Restaurant.Business.ViewModels.Home.About;

namespace Restaurant.Business.Validators.Home.About
{
    public class AboutOptionUpdateVMValidator : AbstractValidator<AboutOptionUpdateVM>
    {
        public AboutOptionUpdateVMValidator()
        {
            RuleFor(x => x.Option).NotEmpty().NotNull().MaximumLength(100);
        }
    }
}
