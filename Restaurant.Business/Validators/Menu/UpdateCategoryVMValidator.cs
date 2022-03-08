using FluentValidation;
using Restaurant.Business.ViewModels.Menu;

namespace Restaurant.Business.Validators.Menu
{
    public class UpdateCategoryVMValidator:AbstractValidator<UpdateCategoryVM>
    {
        public UpdateCategoryVMValidator()
        {
            RuleFor(x=>x.Name).MaximumLength(50);
        }
    }
}
