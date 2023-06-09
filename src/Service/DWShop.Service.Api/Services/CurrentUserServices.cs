using DWShop.Application.Interfaces.Services;
using System.Security.Claims;

namespace DWShop.Service.Api.Services
{
    public class CurrentUserServices : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserServices(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Not found";
        }

        public string UserId { get; }
    }
}
