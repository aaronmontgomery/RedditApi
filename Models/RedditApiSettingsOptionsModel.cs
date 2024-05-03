namespace Models
{
    public class RedditApiSettingsOptionsModel
    {
        public const string RedditApiSettings = "RedditApiSettings";
        
        public string? RedditAccessTokenUrl { get; set; }
        
        public string? RedditApiBaseUrl { get; set; }
    }
}
