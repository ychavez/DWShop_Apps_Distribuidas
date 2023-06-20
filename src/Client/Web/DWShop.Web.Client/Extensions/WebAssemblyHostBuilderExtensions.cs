using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Managers;
using DWShop.Web.Infrastructure.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace DWShop.Web.Client.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {

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
                .AddScoped<DWStateProvider>()
                .AddScoped<AuthenticationStateProvider,DWStateProvider>()
                .AddManagers();

            return builder;
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
