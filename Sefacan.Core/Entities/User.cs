using System;
using System.ComponentModel.DataAnnotations;

namespace Sefacan.Core.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public DateTime? LastActivityDate { get; set; }
    }
}