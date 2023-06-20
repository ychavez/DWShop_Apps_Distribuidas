using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace DWShop.Web.Infrastructure.Authentication
{
    public class DWStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public DWStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(GetClaimsFromJwt(savedToken))));

            return state;
        }

        private IEnumerable<Claim> GetClaimsFromJwt(string jwt)
        {
            byte[] ParseBase64(string payload) 
            {
                payload = payload.Trim().Replace('-', '+').Replace('_', '/');
                var base64 = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
                return Convert.FromBase64String(base64);
            }

            var claims = new List<Claim>();
            var payload = jwt.Split(".")[1];
            var jsonBytes = ParseBase64(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs is not null)
            {
                keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);
                if (roles is not null)
                {
                    if (roles.ToString().Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles!.ToString());
                        claims.AddRange(parsedRoles!.Select(x => new Claim(ClaimTypes.Role, x)));
                    }
                    else
                        claims.Add(new Claim(ClaimTypes.Role, roles!.ToString()));
                }
            }
            return claims;
        }
    }
}

