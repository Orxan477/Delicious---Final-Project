using Restaurant.Core.Interfaces;
using Restaurant.Core.Interfaces.HomeInterfaces;
using Restaurant.Core.Interfaces.ReservationInterfaces;
using Restaurant.Data.DAL;
using Restaurant.Data.Implementations.HomeImplementations;
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
        private AboutGetRepository _aboutGetRepository;
        private AboutCRUDRepository _aboutCRUDRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public  IReservationGetRepository ReservationPaginateRepository => _reservationPaginateRepository
                                                                                    ?? new ReservationGetRepository(_context);

        public IReservationCURDRepository ReservationCRUDRepository => _reservationCRUDRepository ??
                                                                                        new ReservationCRUDRepository(_context);

        public ISettingRepository SettingRepository => _settingRepository ?? new SettingRepository(_context);

        public IAboutGetRepository AboutGetRepository => _aboutGetRepository ?? new AboutGetRepository(_context);

        public IAboutCRUDRepository AboutCRUDRepository => _aboutCRUDRepository ?? new AboutCRUDRepository(_context);

        public async Task SaveChangeAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
    