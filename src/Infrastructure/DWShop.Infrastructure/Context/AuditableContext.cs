﻿using DWShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Context
{
    public abstract class AuditableContext : DbContext
    {
        public AuditableContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Audit> AuditTrails { get; set; }

        public virtual async Task<int> SaveChangesAsync(string userId = null,
            CancellationToken cancellationToken = new())
        {
            var auditEntries = onBeforeSavesChanges(userId);
            var result = await base.SaveChangesAsync(cancellationToken);
            // TODO onbefore saves changes;
            return result;
        }

        private List<AuditEntry> onBeforeSavesChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached
                    || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = userId
                };
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified && property.OriginalValue?.Equals(property.CurrentValue) == false)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditTrails.Add(auditEntry.ToAudit());
            }

            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }
    }
}
