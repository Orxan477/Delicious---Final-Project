using FluentValidation;
using Restaurant.Business.ViewModels.Position;

namespace Restaurant.Business.Validators.Position
{
    public class CreatePositionVMValidation:AbstractValidator<CreatePositionVM>
    {
        public CreatePositionVMValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().NotNull().MaximumLength(20);
        }
    }
}
