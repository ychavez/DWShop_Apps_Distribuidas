using DWShop.Web.Infrastructure.Settings;
using MudBlazor;

namespace DWShop.Web.Client.Shared
{
    public partial class MainLayout
    {
        private MudTheme currentTheme;


        protected override async Task OnInitializedAsync()
        {
            currentTheme = await _clientPreference.GetCurrentThemeAsync();
            await base.OnInitializedAsync();
        }

        public async Task DarkMode() 
        {
            bool isDarkMode = await _clientPreference.ToggleDarkmodeAsync();

            currentTheme = isDarkMode
                ? DWTheme.DefaultTheme
                : DWTheme.DarkTheme;
        }
    }
}
