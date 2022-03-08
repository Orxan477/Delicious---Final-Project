using FluentValidation;
using Restaurant.Business.ViewModels.Position;

namespace Restaurant.Business.Validators.Position
{
    public class UpdatePositionVMValidation:AbstractValidator<UpdatePositionVM>
    {
        public UpdatePositionVMValidation()
        {
            RuleFor(x => x.Name).MaximumLength(20);
        }
    }
}
