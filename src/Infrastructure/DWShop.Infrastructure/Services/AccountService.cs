using DWShop.Application.Interfaces.Services;
using DWShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DWShop.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<DWUser> userManager;
        private readonly IConfiguration configuration;

        public AccountService(UserManager<DWUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<bool> UserExists(string username, CancellationToken cancellationToken)
            => await userManager.Users.AnyAsync(x => x.UserName == username, cancellationToken);


        public async Task<string> GetToken(DWUser user)
        {
            var now = DateTime.UtcNow;
            var key = configuration["Identity:Key"];

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub,user.UserName!),
                new (JwtRegisteredClaimNames.Jti, user.Id),
                new (JwtRegisteredClaimNames.Iat, now.ToLocalTime().ToString(),ClaimValueTypes.Integer64),
                new (JwtRegisteredClaimNames.Email, user.Email ?? "NotDefined")
            };

            var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!));

            var roles = await userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials =
                new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var encodedJwt = new JwtSecurityTokenHandler();

            var token = encodedJwt.CreateToken(tokenDescriptor);

            return encodedJwt.WriteToken(token);
        }
    }
}
