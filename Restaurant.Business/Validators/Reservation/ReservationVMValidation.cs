using FluentValidation;
using Restaurant.Business.ViewModels.Reservation;

namespace Restaurant.Business.Validators.Reservation
{
    public class ReservationVMValidation : AbstractValidator<ReservationVM>
    {
        public ReservationVMValidation()
        {
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().NotNull().MaximumLength(255).EmailAddress();
            RuleFor(x => x.Number).NotEmpty().NotNull();
            RuleFor(x => x.Date).NotEmpty().NotNull();
            RuleFor(x => x.PeopleCount).NotEmpty().NotNull();
        }
    }
}
