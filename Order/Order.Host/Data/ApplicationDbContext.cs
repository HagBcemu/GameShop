using Microsoft.EntityFrameworkCore;
using Order.Host.Data.Entities;
using Order.Host.Data.EntityConfiguration;

namespace Order.Host.Data
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{
			Database.EnsureCreated();
		}

		
		public DbSet<Entities.Order> Orders { get; set; } = null!;
		public DbSet<GameItem> GameItems { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new GameItemConfiguration());
		}
	}
}
