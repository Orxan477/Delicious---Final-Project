using FluentValidation;
using Restaurant.Business.ViewModels.Home.HomeIntro;

namespace Restaurant.Business.Validators.Home.HomeIntro
{
    public class HomeIntroUpdateVMValidator:AbstractValidator<HomeIntroUpdateVM>
    {
        public HomeIntroUpdateVMValidator()
        {
            RuleFor(x => x.Photo).NotEmpty().NotNull();
            RuleFor(x => x.Head).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(x => x.Content).NotEmpty().NotEmpty().MaximumLength(50);
        }
    }
}
