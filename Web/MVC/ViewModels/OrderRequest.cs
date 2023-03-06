namespace MVC.ViewModels
{
	public class OrderRequest
	{	
		public int IdClient { get; set; }

		public decimal Price { get; set; }

		public List<GameItemDto> Items { get; set; } = new List<GameItemDto>();
	}
	public class GameItemDto
	{
		public int GameId { get; set; }
		public int Count { get; set; }
	}
}
