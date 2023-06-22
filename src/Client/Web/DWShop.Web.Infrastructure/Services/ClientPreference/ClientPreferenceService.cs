using Blazored.LocalStorage;
using DWShop.Web.Infrastructure.Settings;
using MudBlazor;

namespace DWShop.Web.Infrastructure.Services.ClientPreference
{
    public class ClientPreferenceService
    {
        private readonly ILocalStorageService localStorageService;

        public ClientPreferenceService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<MudTheme> GetCurrentThemeAsync() 
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference is not null)
                if (preference.IsDarkMode)
                    return DWTheme.DarkTheme;
           
            return DWTheme.DefaultTheme;
        }

        public async Task<bool> ToggleDarkmodeAsync() 
        {

            var preference = await GetPreference() as ClientPreference;
            if (preference is not null)
            {
                preference.IsDarkMode = !preference.IsDarkMode;
                await localStorageService.SetItemAsync<ClientPreference>("clientPreference", preference);
                return !preference.IsDarkMode;
            }

            return false;

        }

        public async Task<IPreference> GetPreference() 
        {
            var preference = await localStorageService.GetItemAsync<ClientPreference>("clientPreference")
                 ?? new ClientPreference();
            return preference;
        }
    }

    public interface IPreference {
    
    }

    public record ClientPreference : IPreference
    {
        public bool IsDarkMode { get; set; }

    }


}
