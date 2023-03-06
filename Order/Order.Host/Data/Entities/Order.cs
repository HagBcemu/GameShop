namespace Order.Host.Data.Entities
{
	public class Order
	{
		public int Id { get; set; }

		public int IdClient { get; set; }

		public decimal Price { get; set; }

		public List<GameItem> Items { get; set; } = new List<GameItem>();
	}
}
