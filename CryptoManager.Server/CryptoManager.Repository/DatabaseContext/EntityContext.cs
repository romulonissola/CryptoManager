using CryptoManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoManager.Repository.DatabaseContext
{
    public class EntityContext : DbContext
    {
        public DbSet<Exchange> Exchange { get; set; }
        public DbSet<Asset> Asset { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
