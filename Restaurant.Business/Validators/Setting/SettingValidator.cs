using FluentValidation;

namespace Restaurant.Business.Validators.Setting
{
    public class SettingValidator:AbstractValidator<Core.Models.Setting>
    {
        public SettingValidator()
        {
            RuleFor(x => x.Value).NotEmpty().NotNull();
        }
    }
}
