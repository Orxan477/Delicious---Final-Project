using FluentValidation;
using Restaurant.Business.ViewModels.Home.HomeIntro;

namespace Restaurant.Business.Validators.Home.HomeIntro
{
    public class HomeIntroCreateVMValidator:AbstractValidator<HomeIntroCreateVM>
    {
        public HomeIntroCreateVMValidator()
        {
            RuleFor(x => x.Photo).NotEmpty().NotNull();
            RuleFor(x => x.Head).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(x=>x.Content).NotEmpty().NotEmpty().MaximumLength(50);
        }
    }
}
