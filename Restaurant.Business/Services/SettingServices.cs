using Restaurant.Data.DAL;
using System.Collections.Generic;

namespace Restaurant.Business.Services
{
    public class SettingServices
    {
        private AppDbContext _context;

        public SettingServices(AppDbContext context)
        {
            _context = context;
        }
        //public Dictionary<string, string> GetSetting()
        //{
        //    return _context.Settings.AsEnumerable().ToDictionary(p => p.Key, p => p.Value);
        //}
    }
}
