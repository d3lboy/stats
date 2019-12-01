using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stats.Common.Dto;

namespace Stats.Fetcher.Library.Clients
{
    public class ApiClient : IApiClient
    {
        private readonly ILogger<ApiClient> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly HttpClient client;

        public ApiClient(ILogger<ApiClient> logger, IOptions<AppConfig> appConfig)
        {
            this.logger = logger;
            this.appConfig = appConfig;
            client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> Get<T>(string action)
        {
            string url = $"{appConfig.Value.ApiUrl}{action}";
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            
            if (!response.IsSuccessStatusCode) return default;

            string str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(str);
        }

        public async Task<bool> Post(string action, BaseDto dto)
        {
            string url = $"{appConfig.Value.ApiUrl}{action}";
            HttpContent content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<T> Post<T>(string action, List<BaseDto> dtos)
        {
            string url = $"{appConfig.Value.ApiUrl}{action}";
            HttpContent content = new StringContent(JsonSerializer.Serialize(dtos), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            if (!response.IsSuccessStatusCode) return default;

            string str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(str);
        }

        public async Task<T> Post<T>(string action, BaseDto dto)
        {
            string url = $"{appConfig.Value.ApiUrl}{action}";
            HttpContent content= new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            if (!response.IsSuccessStatusCode) return default;

            string str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(str);
        }

        public async Task<bool> Put(string action, BaseDto dto)
        {
            string url = $"{appConfig.Value.ApiUrl}{action}";
            HttpContent content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string action)
        {
            string url = $"{appConfig.Value.ApiUrl}{action}";

            var response = await client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
