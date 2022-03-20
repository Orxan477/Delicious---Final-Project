using Restaurant.Core.Interfaces.ReservationInterfaces;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;

namespace Restaurant.Data.Implementations.ReservationImplementations
{
    public class ReservationCRUDRepository:CRUDRepository<Reservation>,IReservationCURDRepository
    {
        private AppDbContext _context { get; }
        public ReservationCRUDRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
