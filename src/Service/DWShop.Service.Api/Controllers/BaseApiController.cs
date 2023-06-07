using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator mediatorInstance;

        protected IMediator mediator => mediatorInstance
            ??= HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new ArgumentNullException(nameof(IMediator));
    }
}
