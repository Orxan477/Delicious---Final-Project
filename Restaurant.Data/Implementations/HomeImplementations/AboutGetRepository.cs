using Restaurant.Core.Interfaces.HomeInterfaces;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;

namespace Restaurant.Data.Implementations.HomeImplementations
{
    public class AboutGetRepository:GetRepository<About>,IAboutGetRepository
    {
        private AppDbContext _context;
        public AboutGetRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
