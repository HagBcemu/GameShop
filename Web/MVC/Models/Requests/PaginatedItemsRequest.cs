namespace MVC.Dtos;

public class PaginatedItemsRequest
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
    
   // public Dictionary<T, int>? Filters { get; set; }
}