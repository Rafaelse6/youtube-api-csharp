using DotNet8YoutubeApi.Models;
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
        public async Task<IActionResult> GetChannelVideos(string? pageToken = null, int maxResults = 50)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "",
                ApplicationName = "MyYoutubeApp"
            });

            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.ChannelId = "UCq8LldVrjqe61KQttZlLW8g";
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchRequest.MaxResults = maxResults;
            searchRequest.PageToken = pageToken;

            var searchResponse = await searchRequest.ExecuteAsync();

            var videoList = searchResponse.Items.Select(item => new VideoDetails
            {
                Title = item.Snippet.Title,
                Link = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                PublishedAt = item.Snippet.PublishedAtDateTimeOffset
            })
                .OrderBy(video => video.PublishedAt)
                .ToList();

            var response = new YouTubeResponse
            {
                Videos = videoList,
                NextPageToken = searchResponse.NextPageToken,
                PrevPageToken = searchResponse.PrevPageToken,
            };

            return Ok(response);
        }
    }
}
