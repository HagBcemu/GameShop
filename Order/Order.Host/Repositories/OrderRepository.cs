using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger<OrderRepository> _logger;

		public OrderRepository(
			 IDbContextWrapper<ApplicationDbContext> dbContext,
			 ILogger<OrderRepository> logger)
		{
			_dbContext = dbContext.DbContext;
			_logger = logger;
		}

		public async Task Add(Data.Entities.Order order)
		{
			var item = await _dbContext.AddAsync(order);

			await _dbContext.SaveChangesAsync();
		}

		public List<Data.Entities.Order> GetByIdAsync(int id)
		{
			var catalogItem = _dbContext.Orders
		  .Where(i => i.Id == id)
		  .Include(i => i.Items)
		  .ToList();

			if (catalogItem == null)
			{
				return null;
			}
			return catalogItem;
		}
	}
}
