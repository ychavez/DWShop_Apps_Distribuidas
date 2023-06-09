using DWShop.Application.Responses.Identity;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Identity.Commands.Register
{
    public class RegisterUserCommand : IRequest<Result<LoginResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gafette { get; set; }
    }
}
