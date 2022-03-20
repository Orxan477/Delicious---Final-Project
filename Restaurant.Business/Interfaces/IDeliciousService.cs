using Restaurant.Business.Interfaces.Home;
using Restaurant.Business.Interfaces.Setting;

namespace Restaurant.Business.Interfaces
{
    public interface IDeliciousService
    {
        public IAboutService AboutService { get; }
        public IReservationService ReservationService { get; }
    }
}
