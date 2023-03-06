using Basket.Host.Models;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task TestAdd(string userId, string data);
    Task<TestGetResponse> TestGet(string userId);
    Task Remove(string userId);
    Task AddItemGame(string userId, ItemGame itemGame);
    Task<List<ItemGame>> GetItems(string userId);
	Task RemoveOneItemInBasket(string userId, int idItem);
}