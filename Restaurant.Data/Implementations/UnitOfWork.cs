using AutoMapper;
using Restaurant.Core.Interfaces;
using Restaurant.Data.DAL;
using System.Threading.Tasks;

namespace Restaurant.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        private ReservationPaginateRepository _reservationPaginateRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public  IReservationPaginateRepository ReservationPaginateRepository => _reservationPaginateRepository= _reservationPaginateRepository
                                                                            ?? new ReservationPaginateRepository(_context);
        public async Task SaveChange()
        {
           await _context.SaveChangesAsync();
        }
    }
}
    