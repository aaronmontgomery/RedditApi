using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Services
{
    public class RedditService(IHttpClientFactory httpClientFactory, IRedditAuthService redditAuthService) : IRedditService
    {
        public HttpClient HttpClient => _httpClientFactory.CreateClient("RedditApiOauthHttpClient");

        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IRedditAuthService _redditAuthService = redditAuthService;
        
        public async Task<T?> Get<T>(HttpClient httpClient, string url, CancellationToken cancellationToken = default)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _redditAuthService.RedditTokenModel!.AccessToken);
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url, cancellationToken);
            T? model = await httpResponseMessage.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<T>(cancellationToken);
            return model;
        }
    }
}
