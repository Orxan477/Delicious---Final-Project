using FluentValidation;
using Restaurant.Business.ViewModels.Footer;

namespace Restaurant.Business.Validators.Footer
{
    public class SubscribeVMValidation:AbstractValidator<SubscribeVM>
    {
        public SubscribeVMValidation()
        {
            RuleFor(x => x.Email).MaximumLength(255).EmailAddress().NotNull().NotEmpty();
        }
    }
}
