using System.ComponentModel.DataAnnotations;

namespace Sefacan.Admin.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [Display(Name = "Şifre")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Şifre minimum 6 haneli olmalıdır!")]
        public string Password { get; set; }
    }
}