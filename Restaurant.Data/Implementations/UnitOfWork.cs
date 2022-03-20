using Restaurant.Core.Interfaces;
using Restaurant.Core.Interfaces.ReservationInterfaces;
using Restaurant.Data.DAL;
using Restaurant.Data.Implementations.ReservationImplementations;
using System.Threading.Tasks;

namespace Restaurant.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        private ReservationGetRepository _reservationPaginateRepository;
        private ReservationCRUDRepository _reservationCRUDRepository;
        private SettingRepository _settingRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public  IReservationGetRepository ReservationPaginateRepository => _reservationPaginateRepository
                                                                                    ?? new ReservationGetRepository(_context);

        public IReservationCURDRepository ReservationCRUDRepository => _reservationCRUDRepository ??
                                                                                        new ReservationCRUDRepository(_context);

        public ISettingRepository SettingRepository => _settingRepository ?? new SettingRepository(_context);

        public async Task SaveChangeAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
    