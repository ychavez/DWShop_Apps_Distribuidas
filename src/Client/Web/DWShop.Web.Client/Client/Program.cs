using DWShop.Web.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DWShop.Web.Client.Extensions;

namespace DWShop.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder
                .CreateDefault(args)
                .AddRootComponents()
                .AddClientServices();
                
            await builder.Build().RunAsync();
        }
    }
}