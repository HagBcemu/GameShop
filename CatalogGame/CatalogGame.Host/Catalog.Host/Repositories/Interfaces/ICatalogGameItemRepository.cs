using CatalogGame.Data;
using CatalogGame.Data.Entities;

namespace CatalogGame.Host.Repositories.Interfaces
{
    public interface ICatalogGameItemRepository
    {
        Task<PaginatedItems<CatalogGameItem>> GetByPageAsync(int pageIndex, int pageSize);

        Task<int?> Add(string name, string description, decimal price,  string pictureFileName, string companyName);

        Task<bool?> UpdateGame(int id, string name, string description, decimal price, string pictureFileName, string companyName);

        Task<bool?> DeleteGame(int id);

        Task<CatalogGameItem?> GetByIdAsync(int id);
    }
}
