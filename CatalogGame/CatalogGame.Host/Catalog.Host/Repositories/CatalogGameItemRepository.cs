using CatalogGame.Data;
using CatalogGame.Data.Entities;
using CatalogGame.Host.Data;
using CatalogGame.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogGame.Host.Repositories
{
    public class CatalogGameItemRepository : ICatalogGameItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogGameItemRepository> _logger;

        public CatalogGameItemRepository(
             IDbContextWrapper<ApplicationDbContext> dbContext,
             ILogger<CatalogGameItemRepository> logger)
        {
            _dbContext = dbContext.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name, string description, decimal price, string pictureFileName, string companyName)
        {
            var item1 = new CatalogGameItem
            {
                CompanyName = companyName,
                Description = description,
                Name = name,
                PictureFileName = pictureFileName,
                Price = price,
            };
            var item = await _dbContext.AddAsync(item1);

            await _dbContext.SaveChangesAsync();
            return item.Entity.Id;
        }

        public async Task<bool?> DeleteGame(int id)
        {
            var removeGame = await _dbContext.CatalogGameItems
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (removeGame == null)
            {
                return false;
            }

            _dbContext.Remove(removeGame);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<CatalogGameItem?> GetByIdAsync(int id)
        {
            var catalogItem = await _dbContext.CatalogGameItems
           .Where(i => i.Id == id)
           .FirstOrDefaultAsync();

            if (catalogItem == null)
            {
                return null;
            }

            return catalogItem;
        }

        public async Task<PaginatedItems<CatalogGameItem>> GetByPageAsync(int pageIndex, int pageSize)
        {
            IQueryable<CatalogGameItem> query = _dbContext.CatalogGameItems;

            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query.OrderBy(c => c.Name)
               .Skip(pageSize * pageIndex)
               .Take(pageSize)
               .ToListAsync();

            return new PaginatedItems<CatalogGameItem>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<bool?> UpdateGame(int id, string name, string description, decimal price, string pictureFileName, string companyName)
        {
            var updateGame = await _dbContext.CatalogGameItems
           .Where(i => i.Id == id).FirstOrDefaultAsync();
            if (updateGame == null)
            {
                return false;
            }

            updateGame!.Name = name;
            updateGame.Description = description;
            updateGame.PictureFileName = pictureFileName;
            updateGame.Price = price;
            updateGame.CompanyName = companyName;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
