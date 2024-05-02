namespace Services
{
    public class RedditAuthService : IRedditAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RedditAuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //HttpClient httpClient = _httpClientFactory.CreateClient(nameof(RedditAuthService));
    }
}
