using Models;

namespace Services
{
    public interface IRedditService
    {
        HttpClient HttpClient { get; }
        
        Task<PopularModel?> GetSubRedditAsync(HttpClient httpClient);
    }
}
