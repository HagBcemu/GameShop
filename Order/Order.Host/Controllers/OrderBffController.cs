using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Model;
using Order.Host.Models;
using Order.Host.Repositories;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderBffController : ControllerBase
{
    private readonly ILogger<OrderBffController> _logger;
    private readonly IOrderService _orderService;
	private readonly IOrderRepository _orderRepository;

	public OrderBffController(
        ILogger<OrderBffController> logger,
        IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddOrder(OrderDto orderAdd)
    {
        int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);

        Data.Entities.Order order = new Data.Entities.Order() 
		{ 
			IdClient = userId,
			Price = orderAdd.Price,
			Items = new List<GameItem>(),	
		};
		if (orderAdd.Items.Count()==0)
		{
            return Ok();
		}

		foreach (var item in orderAdd.Items)
		{
			order.Items.Add(new GameItem() { GameId = item.GameId, Count = item.Count });
		}

		var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
		var options = optionsBuilder
				.UseNpgsql(@"server=www.alevelwebsite.com;port=5433;database=orders2;uid=postgres;password=postgres;")
				.Options;
		
		using (ApplicationDbContext db = new ApplicationDbContext(options))
		{
			var item = await db.AddAsync(order);
			await db.SaveChangesAsync();
		}		
        return Ok();
    }

	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<IActionResult> GetOrders()
	{
		int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);
		var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
		var options = optionsBuilder
				.UseNpgsql(@"server=www.alevelwebsite.com;port=5433;database=orders2;uid=postgres;password=postgres;")
				.Options;

		using (ApplicationDbContext db = new ApplicationDbContext(options))
		{
			var catalogItem = db.Orders
		  .Where(i => i.IdClient == userId)
		  .Include(x => x.Items)
		  .ToList(); 
			
			return Ok(catalogItem);
		}
	}
}