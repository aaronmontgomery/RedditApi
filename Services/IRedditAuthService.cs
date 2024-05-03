using Models;

namespace Services
{
    public interface IRedditAuthService
    {
        HttpClient HttpClient { get; }
        
        RedditTokenModel? RedditTokenModel { get; }
        
        Task SetAccessTokenAsync(HttpClient httpClient, string code, string redirectUrl);
    }
}
