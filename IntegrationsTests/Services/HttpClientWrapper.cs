using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationsTests.Services
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetStringAsync(Uri uri)
        {
            return await _httpClient.GetStringAsync(uri);
        }

        public async Task<string> PostAsync(Uri uri, HttpContent content)
        {
            var response = await _httpClient.PostAsync(uri, content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}