using DWShop.Domain.Contracts;
using DWShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Context
{
    public class DWShopContext : AuditableContext
    {
        public DWShopContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "User";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifieOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "User";
                        break;

                }
            }
            return base.SaveChangesAsync("User",cancellationToken);
        }
    }
}
