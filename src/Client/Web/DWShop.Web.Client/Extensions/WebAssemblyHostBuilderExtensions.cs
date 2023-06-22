using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Managers;
using DWShop.Client.Infrastructure.Routes;
using DWShop.Web.Infrastructure.Authentication;
using DWShop.Web.Infrastructure.Services.Authentication.Login;
using DWShop.Web.Infrastructure.Services.ClientPreference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Reflection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace DWShop.Web.Client.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {

        const string ClientName = "DWShop.Web.Client";
        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");
            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services
                .AddMudServices()
                .AddBlazoredLocalStorage()
                .AddScoped<ClientPreferenceService>()
                .AddScoped<DWStateProvider>()
                .AddScoped<AuthenticationStateProvider, DWStateProvider>()
                .AddManagers()
                .AddScoped<ILoginService, LoginService>()
                .AddAuthorizationCore(options => RegisterPermissionClaims(options))
                .AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient(ClientName).EnableIntercept(sp))

                .AddTransient<AuthenticationHeaderHandler>()

                .AddHttpClient(ClientName, client =>
                {
                    client.BaseAddress = new Uri(BaseConfiguration.BaseAddress);

                })
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();

            builder.Services.AddHttpClientInterceptor();
            return builder;
        }

        private static void RegisterPermissionClaims(AuthorizationOptions options)
        {

        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);

            var types = managers.Assembly.GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Select(x => new
                {
                    Service = x.GetInterface($"I{x.Name}"),
                    Implementation = x
                })
                .Where(x => x.Service is not null);

            foreach (var type in types)
                if (managers.IsAssignableFrom(type.Service))
                    services.AddTransient(type.Service, type.Implementation);
              
            return services;
        }
    }
}
