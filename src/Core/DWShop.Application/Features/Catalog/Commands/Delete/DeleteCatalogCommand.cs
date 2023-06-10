using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Commands.Delete
{
    public class DeleteCatalogCommand : IRequest<IResult>
    {
        public int Id { get; set; }
    }
}
