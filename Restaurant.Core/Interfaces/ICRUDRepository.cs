using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface ICRUDRepository<TEntity>
    {
        Task CreateAsync(TEntity Entity);
        Task UpdateAsync(int id,TEntity Entity);
        Task Delete(int id);
    }
}
