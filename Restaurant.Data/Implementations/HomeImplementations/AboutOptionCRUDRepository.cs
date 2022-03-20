using Restaurant.Core.Interfaces.HomeInterfaces;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;

namespace Restaurant.Data.Implementations.HomeImplementations
{
    public class AboutOptionCRUDRepository:CRUDRepository<AboutOption>,IAboutOptionCRUDRepository
    {
        private AppDbContext _context;
        public AboutOptionCRUDRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
