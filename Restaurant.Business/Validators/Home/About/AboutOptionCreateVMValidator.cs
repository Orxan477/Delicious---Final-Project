using FluentValidation;
using Restaurant.Business.ViewModels.Home.About;

namespace Restaurant.Business.Validators.Home.About
{
    public class AboutOptionCreateVMValidator:AbstractValidator<AboutOptionCreateVM>
    {
        public AboutOptionCreateVMValidator()
        {
            RuleFor(x => x.Option).NotEmpty().NotNull().MaximumLength(200);
        }
    }
}
