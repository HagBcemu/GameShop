namespace Order.Host.Repositories.Interfaces
{
	public interface IOrderRepository
	{
		Task Add(Data.Entities.Order order);
		List<Data.Entities.Order> GetByIdAsync(int id);
	}
}
