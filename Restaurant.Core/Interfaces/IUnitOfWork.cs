using Restaurant.Core.Interfaces.ReservationInterfaces;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IReservationGetRepository ReservationPaginateRepository { get; }
        public IReservationCURDRepository ReservationCRUDRepository { get; }
        public ISettingRepository SettingRepository { get; }
        Task SaveChangeAsync();
    }
}
