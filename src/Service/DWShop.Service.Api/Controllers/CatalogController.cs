using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Catalog.Queries;
using DWShop.Application.Responses.Catalog;
using DWShop.Shared.Wrapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CatalogController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IResult<IEnumerable<ProductResponse>>>>GetAll([FromQuery] GetCatalogQuery query)
            => Ok(await mediator.Send(query));

        [HttpPost]
        public async Task<ActionResult<IResult<int>>> CreateProduct([FromBody]CreateCatalogCommand command)
            => Ok(await mediator.Send(command));
    }
}
