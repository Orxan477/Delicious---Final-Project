using FluentValidation;
using Restaurant.Business.ViewModels.Home;

namespace Restaurant.Business.Validators.Home
{
    public class ContactUsVMValidation:AbstractValidator<ContactUsVM>
    {
        public ContactUsVMValidation()
        {
            RuleFor(x => x.Name).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x=>x.Email).MaximumLength(255).EmailAddress().NotNull().NotEmpty();
            RuleFor(x=>x.Subject).MaximumLength(15);
            RuleFor(x=>x.Message).MaximumLength(255).NotNull().NotEmpty();
        }
    }
}
