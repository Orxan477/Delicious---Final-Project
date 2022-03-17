using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IUnitOfWork
    {
        //public IPaginateRepository Repository { get; }
        Task SaveChange();
    }
}
