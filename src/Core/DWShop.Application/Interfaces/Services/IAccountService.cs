using DWShop.Domain.Entities;

namespace DWShop.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username, CancellationToken cancellationToken);
        Task<string> GetToken(DWUser user);
    }
}
