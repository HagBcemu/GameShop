using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.ViewModels;
namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type)
    {
        var filters = new Dictionary<CatalogTypeFilter, int>();

        if (brand.HasValue)
        {
            filters.Add(CatalogTypeFilter.Brand, brand.Value);
        }
        
        if (type.HasValue)
        {
            filters.Add(CatalogTypeFilter.Type, type.Value);
        }
        
        var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest>($"{_settings.Value.CatalogUrl}/items",
           HttpMethod.Post, 
           new PaginatedItemsRequest()
            {
                PageIndex = page,
                PageSize = take,
                //Filters = filters
            });

        return result;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        await Task.Delay(300);
        var list = new List<SelectListItem>
        {
            new SelectListItem()
            {
                Value = "0",
                Text = "brand 1"
            },
            new SelectListItem()
            {
                Value = "1",
                Text = "brand 2"
            }
        };
        var result = await _httpClient.SendAsync<object, object>($"{_settings.Value.CatalogUrl}/getbrands",
            HttpMethod.Post, new {} );
        
        return list;
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        await Task.Delay(300);
        var list = new List<SelectListItem>
        {
            new SelectListItem()
            {
                Value = "0",
                Text = "type 1"
            },
            
            new SelectListItem()
            {
                Value = "1",
                Text = "type 2"
            }
        };

        return list;
    }

	public async Task<List<ItemGame>> GetBasketItem()
	{
		var result = await _httpClient.SendAsync<List<ItemGame>, object>($"{_settings.Value.BasketUrl}/GetBasket",
          HttpMethod.Post,
		   new { });

		return result;
	}

    public async Task AddToBasket(ItemGame itemGame)
    {
        var result = await _httpClient.SendAsync<object, ItemGame>($"{_settings.Value.BasketUrl}/AddGameItem",
          HttpMethod.Post,
           itemGame);
    }

	public async Task RemoveBasket()
	{
		var result = await _httpClient.SendAsync<object, object>($"{_settings.Value.BasketUrl}/Remove",
		 HttpMethod.Post,
         new { });
	}

	public async Task RemoveOneItemInBasket(int id)
	{
		var result = await _httpClient.SendAsync<object, object>($"{_settings.Value.BasketUrl}/RemoveOneItemInBasket?idItem={id}",
		HttpMethod.Post,
         new { });
	}

	public async Task CreateOrder(List<ItemGame> itemGames)
	{
        if (itemGames.Count()==0)
        {
            return;
        }
        var order = new OrderRequest()
        {
            IdClient = 0,
            Price = itemGames.Sum(x => x.Price * x.Count),
            Items = new List<GameItemDto>(),
        };

        foreach (var item in itemGames)
        {
            var game = new GameItemDto { Count = item.Count, GameId = item.Id };
            order.Items.Add(game);
		}

		var result = await _httpClient.SendAsync<Orders, OrderRequest>($"http://www.alevelwebsite.com:5004/api/v1/OrderBff/AddOrder",
		HttpMethod.Post,
		 order);
	}


	public async Task<List<Orders>> GetOrders()
	{
		var result = await _httpClient.SendAsync<List<Orders>, object>($"http://www.alevelwebsite.com:5004/api/v1/OrderBff/GetOrders",
		HttpMethod.Post,
		 new { });

        return result;
	}
}
