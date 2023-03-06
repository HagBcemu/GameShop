namespace MVC.ViewModels
{
	public class OrdersPage
	{
			public int Id { get; set; }

			public decimal Price { get; set; }

			public List<GameItemPage> Items { get; set; } = new List<GameItemPage>();
		
	}
	public class GameItemPage
	{
		public string Name { get; set; }
		public string PicturePath { get; set; }
		public int Count { get; set; }

	}
}
