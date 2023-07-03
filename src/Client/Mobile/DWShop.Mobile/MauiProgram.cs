using DWShop.Client.Infrastructure.Managers.Authentication;
using DWShop.Client.Infrastructure.Routes;
using DWShop.Mobile.Models;
using DWShop.Mobile.ViewModels;
using DWShop.Mobile.Views;
using Microsoft.Extensions.Logging;

namespace DWShop.Mobile
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
                }).RegisterAppServices()
                .RegisterViewModels()
                .RegisterViews()
                .RegisterManagers()
                .RegisterModels();

            builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(BaseConfiguration.BaseAddress) });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        private static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            _ = mauiAppBuilder.Services.AddSingleton<AppShell>();



            return mauiAppBuilder;
        }
        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            _ = mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
            return mauiAppBuilder;
        }
        private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            _ = mauiAppBuilder.Services.AddTransient<MainPage>();
            _ = mauiAppBuilder.Services.AddTransient<Hello>();

            return mauiAppBuilder;
        }
        private static MauiAppBuilder RegisterModels(this MauiAppBuilder mauiAppBuilder)
        {
            _ = mauiAppBuilder.Services.AddTransient<Item>();

            return mauiAppBuilder;
        }
        private static MauiAppBuilder RegisterManagers(this MauiAppBuilder mauiAppBuilder)
        {
            _ = mauiAppBuilder.Services.AddTransient<IAuthenticationManager, AuthenticationManager>();

            return mauiAppBuilder;
        }

    }
}