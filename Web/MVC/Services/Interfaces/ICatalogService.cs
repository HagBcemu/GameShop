using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface ICatalogService
{
    Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type);
    Task<IEnumerable<SelectListItem>> GetBrands();
    Task<IEnumerable<SelectListItem>> GetTypes();
    Task<List<ItemGame>> GetBasketItem();
    Task AddToBasket(ItemGame itemGame);
    Task RemoveBasket();
	Task RemoveOneItemInBasket(int id);
    Task<List<Orders>> GetOrders();
    Task CreateOrder(List<ItemGame> itemGames);
}
