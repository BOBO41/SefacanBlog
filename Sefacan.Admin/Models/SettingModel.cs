using Sefacan.Core.Enums;
using System.Collections.Generic;

namespace Sefacan.Admin.Models
{
    public class SettingModel
    {
        public string Name { get; set; }
        public SettingType Type { get; set; }
        public IEnumerable<SettingItemModel> Settings { get; set; }
    }

    public class SettingItemModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string SelectedValue { get; set; }
        public Input Type { get; set; }
    }
}