namespace CatalogGame.Host.Models.Dtos
{
    public class CatalogGameItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string PictureFileName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;
    }
}
