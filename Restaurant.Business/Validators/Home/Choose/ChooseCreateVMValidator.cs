using FluentValidation;
using Restaurant.Business.ViewModels.Home.Choose;

namespace Restaurant.Business.Validators
{
    public class ChooseCreateVMValidator:AbstractValidator<ChooseCreateVM>
    {
        public ChooseCreateVMValidator()
        {
            RuleFor(x => x.CardHead).NotEmpty().NotNull().MaximumLength(20);
            RuleFor(x => x.CardContent).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
