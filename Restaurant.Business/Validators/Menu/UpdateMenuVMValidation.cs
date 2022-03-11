using FluentValidation;
using Restaurant.Business.ViewModels.Menu;

namespace Restaurant.Business.Validators.Menu
{
    public class UpdateMenuVMValidation:AbstractValidator<UpdateMenuVM>
    {
        public UpdateMenuVMValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotEmpty().MaximumLength(50);
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("The field is required.").NotNull().WithMessage("The field is required.");
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(255);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).NotEmpty().NotNull();
        }
    }
}
