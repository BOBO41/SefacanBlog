using Sefacan.Core.Entities;
using Sefacan.Core.Enums;
using Sefacan.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sefacan.Service
{
    public class SettingService : ISettingService
    {
        #region Fields
        private readonly IRepository<Setting> settingRepository;
        #endregion

        #region Ctor
        public SettingService(IRepository<Setting> _settingRepository)
        {
            settingRepository = _settingRepository;
        }
        #endregion

        #region Methods
        public Setting GetSetting(string name)
        {
            return settingRepository.Find(x => x.Name == name);
        }

        public IEnumerable<Setting> GetSetting(SettingType type, Input inputType)
        {
            return settingRepository.Get(x => x.Type == type && x.InputType == inputType).ToList();
        }

        public IEnumerable<Setting> GetSettings(SettingType type)
        {
            return settingRepository.Get(x => x.Type == type).ToList();
        }

        public bool UpdateSetting(Setting setting)
        {
            return settingRepository.Update(setting);
        }
        #endregion
    }
}