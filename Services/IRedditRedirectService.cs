using Models;

namespace Services
{
    public interface IRedditRedirectService
    {
        RedirectResponseModel Redirect(HttpClient httpClient, string code);
    }
}
