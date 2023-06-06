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
    }
}
