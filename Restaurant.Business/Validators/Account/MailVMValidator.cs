using FluentValidation;
using Restaurant.Business.ViewModels.Account;

namespace Restaurant.Business.Validators.Account
{
    public class MailVMValidator:AbstractValidator<MailVm>
    {
        public MailVMValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}
