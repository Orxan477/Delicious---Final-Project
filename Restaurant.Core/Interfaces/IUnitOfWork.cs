using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IReservationPaginateRepository ReservationPaginateRepository { get; }
        Task SaveChange();
    }
}
