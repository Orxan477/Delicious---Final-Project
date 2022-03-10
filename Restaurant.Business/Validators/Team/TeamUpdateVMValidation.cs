using FluentValidation;
using Restaurant.Business.ViewModels.Team;

namespace Restaurant.Business.Validators.Team
{
    public class TeamUpdateVMValidator:AbstractValidator<UpdateTeamVM>
    {
        public TeamUpdateVMValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.About).MaximumLength(255);
        }
    }
}
