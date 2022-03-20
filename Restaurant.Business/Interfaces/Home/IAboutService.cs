using Microsoft.AspNetCore.Http;
using Restaurant.Business.Interfaces.Setting;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Business.Interfaces.Home
{
    public interface IAboutService:ISettingService
    {
        bool CheckImageValid(IFormFile file, string type, int size);
        Task<List<About>> GetAll();
        Task<About> Get(int id);
        Task<AboutUpdateVM>GetMap(int id);
        Task  Update(int id, AboutUpdateVM aboutUpdate);

    }
}
