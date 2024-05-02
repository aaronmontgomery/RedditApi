using Microsoft.AspNetCore.Mvc;

namespace Reddit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedditController : ControllerBase
    {
        public RedditController()
        {

        }

        [HttpGet]
        [Route("/test")]
        public IActionResult Test()
        {
            return Ok();
        }
        
        [HttpGet]
        [Route("/redirect")]
        public IActionResult Redirection(string code)
        {

            return Ok();
        }
    }
}
