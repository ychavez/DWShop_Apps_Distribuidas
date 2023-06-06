using DWShop.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWShop.Domain.Entities
{
    [Table(nameof(Catalog), Schema = "cat")]
    public class Catalog : AuditableEntity<int>
    {
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string Category { get; set; } = null!;
        [MaxLength(100)]
        public string Description { get; set; } = null!;
        [MaxLength(200)]
        public string Summary { get; set; } = null!;
        public decimal Price { get; set; }
        public string? PhotoURL { get; set; }
    }
}
