using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface ICreateRepository<TEntity>
    {
        Task CreateAsync(TEntity entity);
    }
}
