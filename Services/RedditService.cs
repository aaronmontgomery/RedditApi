namespace Services
{
    public class RedditService : IRedditService
    {
        private readonly IRedditAuthService _redditAuthService;
        
        public RedditService(IRedditAuthService redditAuthService)
        {
            _redditAuthService = redditAuthService;
        }
        
        public void GetSubReddit()
        {

        }
    }
}
