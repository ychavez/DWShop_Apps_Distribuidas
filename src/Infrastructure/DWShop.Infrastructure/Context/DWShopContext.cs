using DWShop.Application.Interfaces.Services;
using DWShop.Domain.Contracts;
using DWShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Context
{
    public class DWShopContext : AuditableContext
    {
        private readonly ICurrentUserService currentUserService;

        public DWShopContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            this.currentUserService = currentUserService;
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
                        entry.Entity.CreatedBy = currentUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifieOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = currentUserService.UserId;
                        break;

                }
            }
            return base.SaveChangesAsync(currentUserService.UserId, cancellationToken);
        }
    }
}
