using DWShop.Application.Responses.Catalog;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Queries
{
    public class GetCatalogQuery : IRequest<IResult<IEnumerable<ProductResponse>>>
    {
    }
}
