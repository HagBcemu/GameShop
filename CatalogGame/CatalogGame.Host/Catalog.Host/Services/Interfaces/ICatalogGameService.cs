using CatalogGame.Data;
using CatalogGame.Data.Entities;
using CatalogGame.Host.Models.Dtos;
using CatalogGame.Host.Models.Response;

namespace CatalogGame.Host.Services.Interfaces
{
    public interface ICatalogGameService
    {
        Task<int?> AddAsync(string name, string description, decimal price, string pictureFileName, string company);

        Task<PaginatedItemsResponse<CatalogGameItemDto>?> GetCatalogItemsAsync(int pageIndex, int pageSize);

        Task<bool?> DeleteGame(int idGame);

        Task<bool?> UpdateGameAsync(int idGame, string name, string description, decimal price, string pictureFileName, string company);

        Task<CatalogGameItemDto?> GetCatalogbyId(int idGame);
    }
}
