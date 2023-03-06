using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfiguration
{
	public class GameItemConfiguration : IEntityTypeConfiguration<Entities.GameItem>
	{
		public void Configure(EntityTypeBuilder<GameItem> builder)
		{
			builder.HasKey(game => game.Id);

			builder.Property(game => game.Id)
				.IsRequired();

			builder.Property(game => game.OrderId)
				.IsRequired(true);

			builder.Property(game => game.Count)
				.IsRequired(true);

			//builder.HasOne(g => g.Order)
			//	.WithMany(o => o.Items)
			//	.HasForeignKey(o => o.OrderId)
			//	.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
