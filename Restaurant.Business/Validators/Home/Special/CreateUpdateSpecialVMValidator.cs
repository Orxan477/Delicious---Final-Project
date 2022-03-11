using FluentValidation;
using Restaurant.Business.ViewModels.Home.Special;

namespace Restaurant.Business.Validators.Home.Special
{
    public class CreateUpdateSpecialVMValidator:AbstractValidator<CreateUpdateSpecialVM>
    {
        public CreateUpdateSpecialVMValidator()
        {
            RuleFor(x => x.InformationTabHead).NotEmpty().NotNull().MaximumLength(40);
            RuleFor(x => x.InformationTabItalicContent).MaximumLength(50);
            RuleFor(x=>x.InformationTabContent).NotEmpty().WithMessage("The field is required.").NotNull().WithMessage("The field is required.").MaximumLength(255);
        }
    }
}
