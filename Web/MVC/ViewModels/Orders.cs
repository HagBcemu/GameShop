namespace MVC.ViewModels
{
	public class Orders
	{
		public int Id { get; set; }

		public int IdClient { get; set; }

		public decimal Price { get; set; }

		public List<GameItem> Items { get; set; } = new List<GameItem>();
	}
	public class GameItem
	{
		public int Id { get; set; }
		public int GameId { get; set; }
		public int Count { get; set; }
		public int OrderId { get; set; }
	}
}
