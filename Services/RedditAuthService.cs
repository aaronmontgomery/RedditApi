using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Models;

namespace Services
{
    public class RedditAuthService(IHttpClientFactory httpClientFactory, IOptionsMonitor<RedditApiSettingsOptionsModel> optionsMonitor) : IRedditAuthService
    {
        public HttpClient HttpClient => _httpClientFactory.CreateClient("RedditApiAuthHttpClient");
        public RedditTokenModel? RedditTokenModel => _redditTokenModel;

        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IOptionsMonitor<RedditApiSettingsOptionsModel> _optionsMonitor = optionsMonitor;
        private RedditTokenModel? _redditTokenModel;
        
        public async Task SetAccessTokenAsync(HttpClient httpClient, string code, string redirectUrl)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_optionsMonitor.CurrentValue.RedditAccessTokenUrl, new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", redirectUrl }
            }));

            _redditTokenModel = await httpResponseMessage.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<RedditTokenModel>();
        }
    }
}
