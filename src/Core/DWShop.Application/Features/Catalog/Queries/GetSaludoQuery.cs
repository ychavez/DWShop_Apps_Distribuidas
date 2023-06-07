using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Queries
{
    public class GetSaludoQuery: IRequest<IResult<string>>
    {
        public string Nombre { get; set; } = null!;
    }
}
