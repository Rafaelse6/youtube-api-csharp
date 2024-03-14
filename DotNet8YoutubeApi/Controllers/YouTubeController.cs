using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8YoutubeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetChannelVideos()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "",
                ApplicationName = "MyYoutubeApp"
            });

            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.ChannelId = "UCq8LldVrjqe61KQttZlLW8g";
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;

            var searchResponse = await searchRequest.ExecuteAsync();

            return Ok(searchResponse);
        }
    }
}
