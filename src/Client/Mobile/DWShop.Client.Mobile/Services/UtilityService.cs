using DWShop.Shared.Constants;
using System.Net;
using System.Net.Http.Headers;

namespace DWShop.Client.Mobile.Services
{
    public class UtilityService
    {
        private readonly HttpClient httpClient;

        public UtilityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await SecureStorage.Default.GetAsync("Token");

            if (token is null)
                return false;

            httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(StorageConstants.Local.Scheme, token);

            var response = await httpClient.GetAsync("/api/Identity/Check");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
                return false;
            }
           
            else if (response.StatusCode == HttpStatusCode.OK)
                return true;

            else if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception("Internal server error");

            return false;
        }

    }
}
