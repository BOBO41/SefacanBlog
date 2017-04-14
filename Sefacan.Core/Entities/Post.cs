using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sefacan.Core.Entities
{
    public class Post : BaseEntity
    {
        [MaxLength(250)]
        public string PicturePath { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public string ShortContent { get; set; }
        public string FullContent { get; set; }
        public int CategoryId { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool AllowComment { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}