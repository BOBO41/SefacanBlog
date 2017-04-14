using System;
using System.ComponentModel.DataAnnotations;

namespace Sefacan.Framework
{
    [Serializable]
    public abstract class BaseModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
    }
}