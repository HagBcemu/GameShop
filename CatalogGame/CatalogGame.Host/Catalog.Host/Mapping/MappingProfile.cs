using AutoMapper;
using CatalogGame.Data.Entities;
using CatalogGame.Host.Mapping;
using CatalogGame.Host.Models.Dtos;

namespace CatalogGame.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogGameItem, CatalogGameItemDto>()
            .ForMember("PictureFileName", opt
                => opt.MapFrom<CatalogItemPictureResolver, string>(c => c.PictureFileName));
    }
}