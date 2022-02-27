using Restaurant.Core.Interfaces;
using Restaurant.Data.DAL;
using System.Threading.Tasks;

namespace Restaurant.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        //private ProductRepository _productRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        //public IProductRepository productRepository => _productRepository ?? new ProductRepository(_context);

        public async Task SaveChange()
        {
           await _context.SaveChangesAsync();
        }
    }
}
    