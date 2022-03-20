using Restaurant.Core.Interfaces;
using Restaurant.Data.DAL;
using System.Threading.Tasks;

namespace Restaurant.Data.Implementations
{
    public class CRUDRepository<TEntity> : ICRUDRepository<TEntity>
        where TEntity : class
    {
        private AppDbContext _context;

        public CRUDRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
