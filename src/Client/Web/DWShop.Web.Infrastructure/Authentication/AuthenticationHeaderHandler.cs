using Blazored.LocalStorage;
using DWShop.Shared.Constants;
using System.Net.Http.Headers;

namespace DWShop.Web.Infrastructure.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService localStorageService;

        public AuthenticationHeaderHandler(ILocalStorageService localStorageService )
        {
            this.localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization?.Scheme != StorageConstants.Local.Scheme)
            {
                var savedToken = await localStorageService.GetItemAsync<string>(StorageConstants.Local.AuthToken);
                if (!string.IsNullOrWhiteSpace(savedToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue(StorageConstants.Local.Scheme, savedToken);
                }

            }
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
