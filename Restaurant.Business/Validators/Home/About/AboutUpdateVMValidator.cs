using FluentValidation;
using Restaurant.Business.ViewModels.Home.About;

namespace Restaurant.Business.Validators.Home.About
{
    public class AboutUpdateVMValidator:AbstractValidator<AboutUpdateVM>
    {
        public AboutUpdateVMValidator()
        {
            RuleFor(x => x.Head).NotEmpty().NotNull().MaximumLength(100);
        }
    }
}
