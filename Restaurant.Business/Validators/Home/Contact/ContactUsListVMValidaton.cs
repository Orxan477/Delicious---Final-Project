using FluentValidation;
using Restaurant.Business.ViewModels.Home.ContactUs;

namespace Restaurant.Business.Validators.Home.Contact
{
    public class ContactUsListVMValidaton : AbstractValidator<ContactUsListVM>
    {
        public ContactUsListVMValidaton()
        {
            RuleFor(x => x.SendMessage).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
