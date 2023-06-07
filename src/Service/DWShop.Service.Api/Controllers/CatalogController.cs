﻿
using DWShop.Application.Features.Catalog.Queries;
using DWShop.Application.Responses;
using DWShop.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    public class CatalogController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IResult<IEnumerable<ProductResponse>>>>GetAll([FromQuery] GetCatalogQuery query)
            => Ok(await mediator.Send(query));
    }
}
