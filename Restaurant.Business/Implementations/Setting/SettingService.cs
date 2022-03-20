using Restaurant.Business.Interfaces.Setting;
using Restaurant.Core.Interfaces;
using Restaurant.Data.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Business.Implementations.Setting
{
    public class SettingService : ISettingService
    {
        private IUnitOfWork _unitOfWork;
        public SettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetSetting(string key)
        {
            Dictionary<string, string> Settings = _unitOfWork.SettingRepository.GetSetting();
            return Settings[$"{key}"];
        }
    }
}
