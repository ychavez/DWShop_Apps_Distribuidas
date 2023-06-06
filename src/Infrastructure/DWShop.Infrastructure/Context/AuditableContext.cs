using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Context
{
    public abstract class AuditableContext : DbContext
    {
        public AuditableContext(DbContextOptions options) : base(options)
        {
                
        }

    }
}
