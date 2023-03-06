using CatalogGame.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogGame.Data.EntityConfiguration
{
    public class GameEntityConfiguration : IEntityTypeConfiguration<Entities.CatalogGameItem>
    {
        public void Configure(EntityTypeBuilder<Entities.CatalogGameItem> builder)
        {
            builder.ToTable("CatalogGame");

            builder.Property(game => game.Id)
                .UseHiLo("catalogGame_hilo")
                .IsRequired();

            builder.Property(game => game.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(game => game.Price)
                .IsRequired(true);

            builder.Property(game => game.PictureFileName)
                .IsRequired(false);

            builder.Property(game => game.Description)
                .IsRequired(false);

            builder.Property(game => game.CompanyName)
                .IsRequired(false);
        }
    }
}
