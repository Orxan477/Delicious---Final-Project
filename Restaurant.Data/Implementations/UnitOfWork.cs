using Restaurant.Core.Interfaces;
using Restaurant.Data.DAL;
using System.Threading.Tasks;

namespace Restaurant.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        //private PaginateRepository _repository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        //public IPaginateRepository repository => _repository ?? new Repository(_context);

        public async Task SaveChange()
        {
           await _context.SaveChangesAsync();
        }
    }
}
    