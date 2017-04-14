using System;
using System.ComponentModel.DataAnnotations;

namespace Sefacan.Core.Entities
{
    public class Comment : BaseEntity
    {
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [MaxLength(250)]
        public string Content { get; set; }
        public bool IsAccept { get; set; }
        public DateTime CreateDate { get; set; }
        public int PostId { get; set; }
        public int ParentId { get; set; }

        public virtual Post Post { get; set; }
    }
}