using Erfa.ProductionManagement.Domain.Common;
using Erfa.ProductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistence
{
    public class ErfaDbContext : DbContext
    {
        public ErfaDbContext(DbContextOptions<ErfaDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErfaDbContext).Assembly);
            modelBuilder.Entity<Product>().HasKey("ProductNumber");
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var date = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = date;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = date;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
