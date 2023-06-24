using System.Text.Json;

namespace DWShop.Client.Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {

        public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            var json = JsonSerializer.Serialize(value);

            using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri)
            {
                Content = new StringContent(json)
            };

            return await client.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
        }
    }
}
