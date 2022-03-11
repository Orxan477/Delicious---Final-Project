using FluentValidation;
using Restaurant.Business.ViewModels.Home.Special;

namespace Restaurant.Business.Validators.Home.Special
{
    public class CreateUpdateSpecialVMValidator:AbstractValidator<CreateSpecialVM>
    {
        public CreateUpdateSpecialVMValidator()
        {
            RuleFor(x => x.InformationTabHead).NotEmpty().NotNull().MaximumLength(40);
            RuleFor(x => x.InformationTabItalicContent).MaximumLength(50);
            RuleFor(x=>x.InformationTabContent).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
