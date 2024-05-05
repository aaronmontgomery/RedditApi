namespace Services
{
    public interface IRedditService
    {
        HttpClient HttpClient { get; }

        Task<T?> Get<T>(HttpClient httpClient, string url, CancellationToken cancellationToken = default);
    }
}
