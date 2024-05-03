using System.Net.Http.Headers;
using System.Net.Http.Json;
using Models;

namespace Services
{
    public class RedditService(IHttpClientFactory httpClientFactory, IRedditAuthService redditAuthService) : IRedditService
    {
        public HttpClient HttpClient => _httpClientFactory.CreateClient("RedditApiOauthHttpClient");

        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IRedditAuthService _redditAuthService = redditAuthService;
        
        public async Task<PopularModel?> GetSubRedditAsync(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _redditAuthService.RedditTokenModel!.AccessToken);
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://oauth.reddit.com/subreddits/popular");
            PopularModel? popularModel = await httpResponseMessage.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<PopularModel>();
            return popularModel;
        }
    }
}
