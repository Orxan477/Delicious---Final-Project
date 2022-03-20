using System.Collections.Generic;

namespace Restaurant.Core.Interfaces
{
    public interface ISettingRepository
    {
        Dictionary<string, string> GetSetting();
    }
}
