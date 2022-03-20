using Restaurant.Business.Interfaces.Home;

namespace Restaurant.Business.Interfaces
{
    public interface IDeliciousService
    {
        public IAboutService AboutService { get; }
        public IReservationService ReservationService { get; }

    }
}
