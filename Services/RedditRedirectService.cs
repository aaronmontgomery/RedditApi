using Microsoft.Extensions.Options;
using Models;

namespace Services
{
    public class RedditRedirectService(IRedditAuthService redditAuthService, IOptionsMonitor<SettingsOptionsModel> optionsMonitor) : IRedditRedirectService
    {
        private readonly IRedditAuthService _redditAuthService = redditAuthService;
        private readonly IOptionsMonitor<SettingsOptionsModel> _optionsMonitor = optionsMonitor;
        
        public RedirectResponseModel Redirect(HttpClient httpClient, string code)
        {
            _redditAuthService.SetAccessTokenAsync(httpClient, code, _optionsMonitor.CurrentValue.RedirectUrl!);
            return new RedirectResponseModel()
            {
                IsRedirectCompleted = true
            };
        }
    }
}
