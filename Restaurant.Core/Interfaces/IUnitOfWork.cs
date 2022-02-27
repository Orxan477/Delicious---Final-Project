using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IUnitOfWork
    {
        //public IProductRepository productRepository { get; }
        Task SaveChange();
    }
}
