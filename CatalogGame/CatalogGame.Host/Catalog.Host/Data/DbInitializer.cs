using CatalogGame.Data.Entities;
using CatalogGame.Host.Data;

namespace CatalogGame.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.CatalogGameItems.Any())
            {
                await context.CatalogGameItems.AddRangeAsync(GetPreconfiguredCatalogGameItems());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<CatalogGameItem> GetPreconfiguredCatalogGameItems()
        {
            return new List<CatalogGameItem>()
            {
                new CatalogGameItem { CompanyName = "CD Projekt", Description = "The action of the game takes place in 2077 in Night City,", Name = "Cyberpunk 2077", Price = 155, PictureFileName = "1.jpg" },
                new CatalogGameItem { CompanyName = "RockStar", Description = "Open-world action-adventure game", Name = "Grand Theft Auto V", Price = 350, PictureFileName = "2.jpg" },
                new CatalogGameItem { CompanyName = "Rockstar", Description = "Third-person shooter computer game", Name = "Red Dead Redemption 2", Price = 450, PictureFileName = "3.jpg" },
                new CatalogGameItem { CompanyName = "CD Projekt", Description = "The writers and directors had all the tools to make the story brighter", Name = "The Witcher 3: Wild Hunt", Price = 420, PictureFileName = "4.jpg" },
                new CatalogGameItem { CompanyName = "Steam", Description = "Return to Half-Life in VR", Name = "Half-Life: Alyx", Price = 120, PictureFileName = "5.jpg" },
                new CatalogGameItem { CompanyName = "FromSoftware", Description = "Welcom to ez game", Name = "Elden Ring", Price = 320, PictureFileName = "6.jpg" },
                new CatalogGameItem { CompanyName = "Capcom", Description = "Survival horror computer game", Name = "Resident Evil 2: Remake", Price = 800, PictureFileName = "7.jpg" },
                new CatalogGameItem { CompanyName = "Guerrilla Games ", Description = "Action/RPG with Inner World", Name = "Horizon Zero Dawn: The Frozen Wilds", Price = 400, PictureFileName = "8.jpg" },
                new CatalogGameItem { CompanyName = "BlueTwelve Studio", Description = "tray cat must untangle an ancient mystery to escape a city", Name = "Stray", Price = 500, PictureFileName = "9.jpg" },
                new CatalogGameItem { CompanyName = "Sony", Description = "Kratos and Atreus must learn to forgive and not lose heart.", Name = "God of War: Ragnarök", Price = 600, PictureFileName = "10.jpg" },
            };
        }
    }
}
