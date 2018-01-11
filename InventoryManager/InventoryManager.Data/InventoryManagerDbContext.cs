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
            base.OnModelCreating(builder);
        }
    }
}
