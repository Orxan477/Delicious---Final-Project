using Restaurant.Core.Interfaces.HomeInterfaces;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;

namespace Restaurant.Data.Implementations.HomeImplementations
{
    public class AboutOptionGetRepository:GetRepository<AboutOption>,IAboutOptionGetRepository
    {
        private AppDbContext _context;
        public AboutOptionGetRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
