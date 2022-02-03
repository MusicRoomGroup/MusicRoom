using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using MusicRoom.Core.Models;
using MusicRoom.Core.Services.Interfaces;
using static Google.Apis.YouTube.v3.SearchResource;

namespace MusicRoom.Core.Services.Implementations
{
    public class YoutubeSearchService : IYoutubeSearchService
    {
        private readonly YouTubeService _youtube;

        public YoutubeSearchService()
        {
            _youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "REPLACE_ME",
			    ApplicationName = "MusicRoom"
            });
        }

        public async Task<PagedResult<YouTubeVideo>> SearchVideosAsync(string query)
        {
            var videoPage = new PagedResult<YouTubeVideo>();

            ListRequest request = _youtube.Search.List("snippet");

            request.Q = query;

            request.MaxResults = 50;

            return BuildVideoPage(await request.ExecuteAsync());
	    }

        public async Task<PagedResult<YouTubeVideo>> GetNextPageAsync(string token) 
	    {
            ListRequest request = _youtube.Search.List("snippet");

            request.PageToken = token;

            return BuildVideoPage(await request.ExecuteAsync());
	    }

        private PagedResult<YouTubeVideo> BuildVideoPage(SearchListResponse response)
        { 
            return new PagedResult<YouTubeVideo>
            {
                Next = response.NextPageToken,
                Previous = response.PrevPageToken,
                Results = response.Items
						    .Where(itm => itm.Id.Kind == "youtube#video")
						    .Select(BuildVideoModel)
            };
	    }

        private YouTubeVideo BuildVideoModel(SearchResult result) 
	    { 
            return new YouTubeVideo
			{
			    Id = result.Id.VideoId,
			    Title = result.Snippet.Title,
			    Description = result.Snippet.Description,
			    ImageUri = result.Snippet.Thumbnails.Default__.Url,
			};
    	}
    }
}
