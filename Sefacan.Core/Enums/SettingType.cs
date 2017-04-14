using System.ComponentModel.DataAnnotations;

namespace Sefacan.Core.Enums
{
    public enum SettingType
    {
        [Display(Name = "Genel")]
        General,
        [Display(Name = "Seo")]
        Seo,
        [Display(Name = "Mail")]
        Mail,
        [Display(Name = "Yazılar")]
        Post,
        [Display(Name = "Sosyal Medya")]
        Social
    }
}