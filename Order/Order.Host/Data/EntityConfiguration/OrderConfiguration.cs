using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfiguration
{
	public class OrderConfiguration : IEntityTypeConfiguration<Entities.Order>
	{
		public void Configure(EntityTypeBuilder<Entities.Order> builder)
		{
			builder.HasKey(order => order.Id);

			builder.Property(order => order.Id)
				.IsRequired();

			builder.Property(order => order.IdClient)
				.IsRequired(true);

			builder.Property(order => order.Price)
				.IsRequired(true);
		}
	}
}
