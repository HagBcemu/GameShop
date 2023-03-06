namespace CatalogGame.Host.Models.Response
{
    public class AddGameResponce<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
