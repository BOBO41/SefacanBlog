using System.ComponentModel.DataAnnotations;

namespace Sefacan.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gerekli!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre gerekli!")]
        public string Password { get; set; }
    }
}