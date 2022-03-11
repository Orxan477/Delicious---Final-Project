using FluentValidation;
using Restaurant.Business.ViewModels.Menu;

namespace Restaurant.Business.Validators.Menu
{
    public class CreateMenuVMValidation:AbstractValidator<CreateMenuVM>
    {
        public CreateMenuVMValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotEmpty().MaximumLength(50);
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("The field is required.").NotNull().WithMessage("The field is required.");
            RuleFor(x=>x.Description).NotEmpty().NotNull().MaximumLength(255);
            RuleFor(x => x.Photo).NotNull().NotEmpty();
            RuleFor(x=>x.Price).GreaterThanOrEqualTo(0).NotEmpty().NotNull();
        }
    }
}
