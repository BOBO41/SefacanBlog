using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sefacan.Admin.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        [Display(Name = "Aktiflik")]
        public bool IsActive { get; set; }

        [Display(Name = "Kategori Linki")]
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Url { get; set; }
    }
}