using DWShop.Application.Features.Catalog.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController: ControllerBase
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/Saludo")]
        public async Task<ActionResult<string>> getSaludo([FromQuery] GetSaludoQuery query)
            => Ok(await mediator.Send(query));
    }
}
