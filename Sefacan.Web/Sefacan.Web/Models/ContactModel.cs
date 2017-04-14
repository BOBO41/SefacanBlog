using System.ComponentModel.DataAnnotations;

namespace Sefacan.Web.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage ="Zorunlu alan !")]
        [MaxLength(50, ErrorMessage = "Maksimum 50 karakter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zorunlu alan !")]
        [MaxLength(50, ErrorMessage = "Maksimum 50 karakter")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu alan !")]
        [MaxLength(500,ErrorMessage ="Maksimum 500 karakter")]
        public string Message { get; set; }
    }
}