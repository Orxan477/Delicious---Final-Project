using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface ICRUDRepository<TEntity>
    {
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
