using Basket.Host.Models;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
[AllowAnonymous]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<IActionResult> AddGameItem(ItemGame data)
	{
		var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		if (basketId== null)
		{
			basketId = "3";
		}		
		await _basketService.AddItemGame(basketId!, data);
		return Ok();
	}

	[HttpPost]
	[ProducesResponseType(typeof(List<ItemGame>), (int)HttpStatusCode.OK)]
	public async Task<IActionResult> GetBasket()
	{
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		if (basketId == null)
		{
			basketId = "3";
		}
		var response = await _basketService.GetItems(basketId!);
		return Ok(response);
	}

	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<IActionResult> Remove()
	{
		var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		if (basketId == null)
		{
			basketId = "3";
		}
		await _basketService.Remove(basketId!);
		return Ok();
	}

	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<IActionResult> RemoveOneItemInBasket(int idItem)
	{
		var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		if (basketId == null)
		{
			basketId = "3";
		}
		await _basketService.RemoveOneItemInBasket(basketId, idItem);
		return Ok();
	}
}