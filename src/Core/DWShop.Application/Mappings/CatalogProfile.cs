using AutoMapper;
using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Responses;
using DWShop.Domain.Entities;

namespace DWShop.Application.Mappings
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<Catalog, ProductResponse>().ReverseMap();
            CreateMap<Catalog, CreateCatalogCommand>().ReverseMap();
        }
    }
}
