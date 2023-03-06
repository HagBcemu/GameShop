using Infrastructure.Identity;

namespace CatalogGame.Host.Configurations;

public class CatalogConfig
{
    public string CdnHost { get; set; } = null!;
    public string ImgUrl { get; set; } = null!;

    public string ConnectionString { get; set; } = null!;
}