namespace Basket.Host.Models
{
	public class ItemGame
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public decimal Price { get; set; }

		public string PictureFileName { get; set; } = null!;

		public int Count { get; set; }
	}
}
