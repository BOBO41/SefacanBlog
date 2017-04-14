using Sefacan.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sefacan.Core.Entities
{
    public class UrlRecord : BaseEntity
    {
        [MaxLength(500)]
        public string UniqueUrl { get; set; }
        public EntityType EntityType { get; set; }
        public int EntityId { get; set; }
    }
}