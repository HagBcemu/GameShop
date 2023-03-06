namespace CatalogGame.Host.Models.Response
{
    public class StatusOperationResponce<T>
    {
        public T Success { get; set; } = default(T) !;
    }
}
