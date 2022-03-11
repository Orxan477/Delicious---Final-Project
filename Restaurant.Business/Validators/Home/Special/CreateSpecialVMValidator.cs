using FluentValidation;
using Restaurant.Business.ViewModels.Home.Special;

namespace Restaurant.Business.Validators.Home.Special
{
    public class CreateSpecialVMValidator:AbstractValidator<CreateSpecialVM>
    {
        public CreateSpecialVMValidator()
        {
            //RuleFor(x=>x)
        }
    }
}
