using Blazored.LocalStorage;
using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Client.Infrastructure.Managers.Authentication;
using DWShop.Shared.Constants;
using DWShop.Shared.Wrapper;
using DWShop.Web.Infrastructure.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace DWShop.Web.Infrastructure.Services.Authentication.Login
{
    public class LoginService : ILoginService
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly HttpClient httpClient;

        public LoginService(IAuthenticationManager authenticationManager,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider,
            HttpClient httpClient)
        {
            this.authenticationManager = authenticationManager;
            this.localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
            this.httpClient = httpClient;
        }

        public async Task<IResult> Login(LoginCommand model)
        {
            var result = await authenticationManager.Login(model);

            if (result.Succeded)
            {
                await localStorageService.SetItemAsStringAsync(StorageConstants.Local.AuthToken, result.Data.Token);

                await ((DWStateProvider)authenticationStateProvider).StateChangedAsync();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(StorageConstants.Local.Scheme, result.Data.Token);

                return await Result.SuccessAsync();
            }

            return await Result.FailAsync(result.Messages);
        }
    }
}
