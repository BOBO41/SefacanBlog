using System;
using System.ComponentModel.DataAnnotations;

namespace Sefacan.Admin.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [MaxLength(250)]
        [Display(Name = "Görsel")]
        public string PicturePath { get; set; }

        [MaxLength(200)]
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Title { get; set; }

        [MaxLength(200)]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Özet")]
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string ShortContent { get; set; }

        [Display(Name = "İçerik")]
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string FullContent { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori seçin")]
        public int CategoryId { get; set; }

        [Display(Name = "Görüntülenme")]
        public int ViewCount { get; set; }

        [Display(Name = "Aktiflik")]
        public bool IsActive { get; set; }

        [Display(Name = "Yorum Açık")]
        public bool AllowComment { get; set; }

        public bool IsDelete { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Yazı Linki")]
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Url { get; set; }
    }

    public class PostGridModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}