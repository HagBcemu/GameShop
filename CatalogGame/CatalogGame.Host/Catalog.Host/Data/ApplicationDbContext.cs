using Microsoft.EntityFrameworkCore;
using CatalogGame.Data.Entities;
using CatalogGame.Data.EntityConfiguration;

namespace CatalogGame.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<CatalogGameItem> CatalogGameItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
        }
    }
}
