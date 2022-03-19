using Restaurant.Core.Interfaces;
using Restaurant.Data.DAL;
using System.Threading.Tasks;

namespace Restaurant.Data.Implementations
{
    public class CreateRepository<TEntity> : ICreateRepository<TEntity>
        where TEntity : class
    {
        private AppDbContext _context;

        public CreateRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(TEntity entity)
        {
          await _context.Set<TEntity>().AddAsync(entity);
        }
    }
}
