using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Application.Features.Identity.Commands.Register;
using DWShop.Application.Responses.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    public class IdentityController : BaseApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command)
            => Ok(await mediator.Send(command));

        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterUserCommand command)
            => Ok(await mediator.Send(command));

    }
}
