using FluentValidation;
using Restaurant.Business.ViewModels.Home.Feedback;

namespace Restaurant.Business.Validators.Home.Feedback
{
    public class UpdateFeedbackVMValidator:AbstractValidator<UpdateFeedbackVM>
    {
        public UpdateFeedbackVMValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(20);
            RuleFor(x => x.Photo).NotEmpty().NotNull();
            RuleFor(x => x.Comment).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(x => x.PositionId).NotEmpty().WithMessage("The field is required.").NotNull().WithMessage("The field is required.");
        }
    }
}
