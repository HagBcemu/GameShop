using AutoMapper;
using CatalogGame.Data.Entities;
using CatalogGame.Host.Configurations;
using CatalogGame.Host.Models.Dtos;
using Microsoft.Extensions.Options;

namespace CatalogGame.Host.Mapping;

public class CatalogItemPictureResolver : IMemberValueResolver<CatalogGameItem, CatalogGameItemDto, string, object>
{
    private readonly CatalogConfig _config;

    public CatalogItemPictureResolver(IOptionsSnapshot<CatalogConfig> config)
    {
        _config = config.Value;
    }

    public object Resolve(CatalogGameItem source, CatalogGameItemDto destination, string sourceMember, object destMember, ResolutionContext context)
    {
        return $"{_config.CdnHost}/{_config.ImgUrl}/{sourceMember}";
    }
}