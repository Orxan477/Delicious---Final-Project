using FluentValidation;
using Restaurant.Business.ViewModels.Home;

namespace Restaurant.Business.Validators
{
    public class ContactUsValidation:AbstractValidator<ContactUsVM>
    {
        public ContactUsValidation()
        {
            RuleFor(x => x.Name).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x=>x.Email).MaximumLength(255).NotNull().NotEmpty().EmailAddress();
            RuleFor(x=>x.Subject).MaximumLength(20);
            RuleFor(x=>x.Message).MaximumLength(255).NotNull().NotEmpty();
        }
    }
}
