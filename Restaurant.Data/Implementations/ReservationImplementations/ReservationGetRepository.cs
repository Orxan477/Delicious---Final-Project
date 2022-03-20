using Restaurant.Core.Interfaces.ReservationInterfaces;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;

namespace Restaurant.Data.Implementations.ReservationImplementations
{
    public class ReservationGetRepository:GetRepository<Reservation>,IReservationGetRepository
    {
        private readonly AppDbContext _context;
        public ReservationGetRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}