using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Order.Host.Data.Entities
{
	public class GameItem
	{
		public int Id { get; set; }
		public int GameId { get; set; }
		public int Count { get; set; }
		public int OrderId { get; set; }
		//public Order? Order { get; set; }
	}
}
