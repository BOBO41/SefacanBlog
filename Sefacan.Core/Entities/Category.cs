using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sefacan.Core.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
