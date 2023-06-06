using DWShop.Infrastructure.Context;
using DWShop.Infrastructure.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Service.Api.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    
    public class CatalogController:ControllerBase
    {
        private readonly DWShopContext dWShopContext;

        public CatalogController(DWShopContext dWShopContext)
        {
            this.dWShopContext = dWShopContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<catalog>>> get()
            =>Ok( await dWShopContext.Catalogs.ToListAsync());

    }
}
