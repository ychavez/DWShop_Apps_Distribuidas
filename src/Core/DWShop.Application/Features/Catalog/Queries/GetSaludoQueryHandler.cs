using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Queries
{
    public class GetSaludoQueryHandler : IRequestHandler<GetSaludoQuery, IResult<string>>
    {
        public async Task<IResult<string>> Handle(GetSaludoQuery request, CancellationToken cancellationToken)
        {
            if (request.Nombre == "Julian")
                return await Result<string>.FailAsync("No se puede saludar a Julian");

            return await Result<string>.SuccessAsync($"Hola {request.Nombre}","");

        }
    }
}
