using Restaurant.Business.Interfaces.Setting;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Business.Interfaces.Home
{
    public interface IAboutOptionService:ISettingService
    {
        Task<List<AboutOption>> GetAll();
        Task<AboutOption> Get(int id);
        Task<AboutOptionUpdateVM> GetMap(int id);
        Task Create(AboutOptionCreateVM aboutOptionCreate);
        Task Update(int id, AboutOptionUpdateVM aboutUpdate);
        Task Delete(int id);
    }
}
