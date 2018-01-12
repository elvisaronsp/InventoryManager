namespace InventoryManager.Data
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class InventoryManagerDbContext : IdentityDbContext<User>
    {
        public InventoryManagerDbContext(DbContextOptions<InventoryManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clothes> Clothes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Clothes>()
                .HasOne(c => c.Owner)
                .WithMany(o => o.Clothes)
                .HasForeignKey(c => c.OwnerId);

            base.OnModelCreating(builder);
        }
    }
}
