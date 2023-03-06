namespace Basket.Host.Models
{
	public class BasketItemGame<T>
	{
		public string Id { get; set; } = null!;

		public IEnumerable<T>? Data { get; set;}
	}
}
