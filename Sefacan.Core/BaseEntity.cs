using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sefacan.Core
{
    [Serializable]
    public abstract class BaseEntity
    {
        [Key]
        [ScaffoldColumn(false)]
        [Column("Id", Order = 0)]
        public int Id { get; set; }
    }
}