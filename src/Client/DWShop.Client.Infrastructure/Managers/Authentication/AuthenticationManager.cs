using System.Net.Http.Json;
using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Application.Responses.Identity;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Client.Infrastructure.Routes.Authentication;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly HttpClient _httpClient;

        public AuthenticationManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<LoginResponse>> Login(LoginCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync(AuthenticationEndpoints.Login, command);
            var result = await response.ToResult<LoginResponse>();
            return result;
        }
    }
}
