using Restaurant.Core.Interfaces.HomeInterfaces;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;

namespace Restaurant.Data.Implementations.HomeImplementations
{
    public class AboutCRUDRepository:CRUDRepository<About>,IAboutCRUDRepository
    {
        private AppDbContext _context;
        public AboutCRUDRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
