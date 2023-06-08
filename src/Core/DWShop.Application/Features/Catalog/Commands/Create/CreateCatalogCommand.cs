using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Commands.Create
{
    public class CreateCatalogCommand : IRequest<IResult<int>>
    {
        public string Name { get; set; } 
        public string Category { get; set; } 
        public string Description { get; set; } 
        public string Summary { get; set; } 
        public decimal Price { get; set; }
        public string? PhotoURL { get; set; }
    }
}
