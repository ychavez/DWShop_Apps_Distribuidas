using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DWShop.Domain.Entities
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; set; }
        public string UserId { get; set; }
        public string TableName { get; set; }

        public Dictionary<string, object> KeyValues { get; set; } = new();
        public Dictionary<string, object> OldValues { get; set; } = new();
        public Dictionary<string, object> NewValues { get; set; } = new();

        public List<PropertyEntry> TemporaryProperties { get; } = new();
        public AuditType AuditType { get; set; }

        public List<string> ChangedColumns { get; } = new();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

    }

    public enum AuditType : byte
    {
        none = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
