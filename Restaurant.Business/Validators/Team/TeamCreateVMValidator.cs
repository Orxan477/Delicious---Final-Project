using FluentValidation;
using Restaurant.Business.ViewModels.Team;

namespace Restaurant.Business.Validators.Team
{
    public class TeamCreateVMValidator:AbstractValidator<TeamCreateVM>
    {
        public TeamCreateVMValidator()
        {
            RuleFor(x=>x.FullName).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.Photo).NotNull();
            RuleFor(x=>x.About).MaximumLength(255);
        }
    }
}
