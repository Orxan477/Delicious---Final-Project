using FluentValidation;
using Restaurant.Business.ViewModels.Home.Choose;

namespace Restaurant.Business.Validators.Home.Choose
{
    public class ChooseUpdateVMValidator:AbstractValidator<ChooseUpdateVM>
    {
        public ChooseUpdateVMValidator()
        {
            RuleFor(x => x.CardHead).NotEmpty().NotNull().MaximumLength(20);
            RuleFor(x => x.CardContent).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
