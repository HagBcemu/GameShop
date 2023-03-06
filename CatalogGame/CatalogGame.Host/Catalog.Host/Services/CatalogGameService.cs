using AutoMapper;
using CatalogGame.Data;
using CatalogGame.Data.Entities;
using CatalogGame.Host.Data;
using CatalogGame.Host.Models.Dtos;
using CatalogGame.Host.Models.Response;
using CatalogGame.Host.Repositories;
using CatalogGame.Host.Repositories.Interfaces;
using CatalogGame.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace CatalogGame.Host.Services
{
    public class CatalogGameService : BaseDataService<ApplicationDbContext>, ICatalogGameService
    {
        private readonly ICatalogGameItemRepository _cataloGameItemRepository;
        private readonly IMapper _mapper;

        public CatalogGameService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogGameItemRepository cataloGameItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
        {
            _cataloGameItemRepository = cataloGameItemRepository;
            _mapper = mapper;
        }

        public Task<int?> AddAsync(string name, string description, decimal price, string pictureFileName, string company)
        {
            return ExecuteSafeAsync(() => _cataloGameItemRepository.Add(name, description, price, pictureFileName, company));
        }

        public Task<bool?> DeleteGame(int idGame)
        {
            return ExecuteSafeAsync(() => _cataloGameItemRepository.DeleteGame(idGame));
        }

        public async Task<CatalogGameItemDto?> GetCatalogbyId(int idGame)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _cataloGameItemRepository.GetByIdAsync(idGame);

                return _mapper.Map<CatalogGameItemDto>(result);
            });
        }

        public async Task<PaginatedItemsResponse<CatalogGameItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _cataloGameItemRepository.GetByPageAsync(pageIndex, pageSize);
                return new PaginatedItemsResponse<CatalogGameItemDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<CatalogGameItemDto>(s)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public Task<bool?> UpdateGameAsync(int idGame, string name, string description, decimal price, string pictureFileName, string company)
        {
            return ExecuteSafeAsync(() => _cataloGameItemRepository.UpdateGame(idGame, name, description, price, pictureFileName, company));
        }
    }
}
