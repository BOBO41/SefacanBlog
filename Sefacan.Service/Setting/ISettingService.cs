using Sefacan.Core.Entities;
using Sefacan.Core.Enums;
using System.Collections.Generic;

namespace Sefacan.Service
{
    public interface ISettingService
    {
        Setting GetSetting(string name);
        IEnumerable<Setting> GetSetting(SettingType type, Input inputType);
        IEnumerable<Setting> GetSettings(SettingType type);
        bool UpdateSetting(Setting setting);
    }
}
