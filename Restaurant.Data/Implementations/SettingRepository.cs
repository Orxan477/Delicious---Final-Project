using Restaurant.Core.Interfaces;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Data.Implementations
{
    public class SettingRepository : ISettingRepository
    {
        private AppDbContext _context;

        public SettingRepository(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetSetting()
        {
            return _context.Settings.AsEnumerable().ToDictionary(p => p.Key, p => p.Value);
        }
    }
}
