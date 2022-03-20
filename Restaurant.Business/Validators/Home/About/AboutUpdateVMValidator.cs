using FluentValidation;
using Restaurant.Business.ViewModels.Home.About;

namespace Restaurant.Business.Validators.Home.About
{
    public class AboutUpdateVMValidator:AbstractValidator<AboutUpdateVM>
    {
        public AboutUpdateVMValidator()
        {
            RuleFor(x => x.Head).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.NormalContent).NotEmpty().NotNull().MaximumLength(200);  
            RuleFor(x => x.ItalicContent).NotEmpty().NotNull().MaximumLength(150);  
            RuleFor(x => x.NormalContent2).NotEmpty().NotNull().MaximumLength(200);  
        }
    }
}
