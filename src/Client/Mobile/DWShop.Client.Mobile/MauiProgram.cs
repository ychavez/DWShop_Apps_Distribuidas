using DWShop.Client.Infrastructure.Managers.Authentication;
using DWShop.Client.Infrastructure.Routes;
using Microsoft.Extensions.Logging;

namespace DWShop.Client.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterManagers();

            builder.Services.AddScoped(sp => new HttpClient()
                { BaseAddress = new Uri(BaseConfiguration.BaseAddress) });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterManagers(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            return mauiAppBuilder;
        }
    }
}