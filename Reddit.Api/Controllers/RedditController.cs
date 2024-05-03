using Microsoft.AspNetCore.Mvc;
using Services;

namespace Reddit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedditController(IRedditAuthService redditAuthService, IRedditRedirectService redditRedirectService) : ControllerBase
    {
        private readonly IRedditAuthService _redditAuthService = redditAuthService;
        private readonly IRedditRedirectService _redditRedirectService = redditRedirectService;
        
        [HttpGet]
        [Route("/redirect")]
        public IActionResult Redirection(string code)
        {
            IActionResult actionResult;
            
            try
            {
                actionResult = Ok(_redditRedirectService.Redirect(_redditAuthService.HttpClient, code));
            }

            catch (Exception exception)
            {
                actionResult = BadRequest(exception.Message);
            }

            return actionResult;
        }

        [HttpGet]
        [Route("/test")]
        public IActionResult Test() => Ok();
    }
}
