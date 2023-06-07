using DWShop.Application.Features.Catalog.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    public class CatalogController : BaseApiController
    {
        [HttpGet("/Saludo")]
        public async Task<ActionResult<string>> getSaludo([FromQuery] GetSaludoQuery query)
            => Ok(await mediator.Send(query));
    }
}
