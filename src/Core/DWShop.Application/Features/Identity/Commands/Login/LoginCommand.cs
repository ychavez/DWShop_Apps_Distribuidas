using DWShop.Application.Responses.Identity;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Identity.Commands.Login
{
    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
