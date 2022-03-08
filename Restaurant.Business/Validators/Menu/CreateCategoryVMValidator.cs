using FluentValidation;
using Restaurant.Business.ViewModels.Menu;

namespace Restaurant.Business.Validators.Menu
{
    public class CreateCategoryVMValidator : AbstractValidator<CategoryCreateVM>
    {
        public CreateCategoryVMValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(50);
        }
    }
}
