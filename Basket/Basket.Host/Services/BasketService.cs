using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Microsoft.AspNetCore.CookiePolicy;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;
    private readonly IBus _bus;

    public BasketService(
        ICacheService cacheService,
        IBus bus)
    {
        _cacheService = cacheService;
        _bus = bus;
    }
    public async Task AddItemGame(string userId, ItemGame itemGame)
    {
        var basketStatus = await _cacheService.GetAsync<List<ItemGame>>(userId);
        if (basketStatus == null)
        {
            List<ItemGame> items = new List<ItemGame>() { itemGame };

            await _cacheService.AddOrUpdateAsync<List<ItemGame>>(userId, items);
        }
        else
        {
            if(basketStatus.Any(x => x.Id == itemGame.Id))
            {
                basketStatus.Where(x => x.Id == itemGame.Id).First().Count++;
            }
            else
            {
                basketStatus.Add(itemGame);
            }
            await _cacheService.AddOrUpdateAsync<List<ItemGame>>(userId, basketStatus);
        }
    }

    public async Task<List<ItemGame>> GetItems(string userId)
	{
		var basketStatus = await _cacheService.GetAsync<List<ItemGame>>(userId);
        if (basketStatus == null)
        { return new List<ItemGame>();
        }
        return basketStatus;
	}

	public async Task TestAdd(string userId, string data)
    {
       await _cacheService.AddOrUpdateAsync(userId, data);
    }

    public async Task<TestGetResponse> TestGet(string userId)
    {
        var result = await _cacheService.GetAsync<string>(userId);
        return new TestGetResponse() { Data = result };
    }

    public async Task Remove(string userId)
    {
		await _cacheService.RemoveAsync(userId);
	}

	public async Task RemoveOneItemInBasket(string userId, int idItem)
	{
		var basketStatus = await _cacheService.GetAsync<List<ItemGame>>(userId);
		if (basketStatus == null)
		{
            return;
		}
		else
		{
			if (basketStatus.Any(x => x.Id == idItem))
			{
				basketStatus.Where(x => x.Id == idItem).First().Count--;
                if (basketStatus.Where(x => x.Id == idItem).First().Count < 1)
                {
                    basketStatus.Remove(basketStatus.Where(x => x.Id == idItem).First());
				}
			}
			else
			{
                return;
			}
			await _cacheService.AddOrUpdateAsync<List<ItemGame>>(userId, basketStatus);
		}
	}
}