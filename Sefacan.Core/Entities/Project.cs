using System.ComponentModel.DataAnnotations;

namespace Sefacan.Core.Entities
{
    public class Project : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }
    }
}