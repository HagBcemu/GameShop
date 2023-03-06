using Order.Host.Data.Entities;

namespace Order.Host.Model
{
	public class OrderDto
	{
		public int IdClient { get; set; }

		public decimal Price { get; set; }

		public List<GameItemDto> Items { get; set; } = new List<GameItemDto>();
	}
}
