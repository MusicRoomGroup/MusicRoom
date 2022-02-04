using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.Util;
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

        private readonly int _maxResults = 50;

        public YoutubeSearchService()
        {
            _youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyA8ag7gsspTeya6fg6D_piY_vwhJPKsPE4",
			    ApplicationName = "MusicRoom"
            });
        }

        public async Task<PagedResult<YouTubeVideoListItem>> SearchVideosAsync(string query)
        {
            ListRequest request = _youtube.Search.List(new string[] { "snippet", "contentDetails" });

            request.RegionCode = "US";

            request.Q = query;

            request.MaxResults = _maxResults;

            return BuildVideoPage(await request.ExecuteAsync());
	    }

        public async Task<PagedResult<YouTubeVideoListItem>> GetNextPageAsync(string token) 
	    {
            ListRequest request = _youtube.Search.List(new string[] { "snippet", "contentDetails" });

            request.PageToken = token;

            request.RegionCode = "US";

            request.MaxResults = _maxResults;

            return BuildVideoPage(await request.ExecuteAsync());
	    }

        private PagedResult<YouTubeVideoListItem> BuildVideoPage(SearchListResponse response)
        { 
            return new PagedResult<YouTubeVideoListItem>
            {
                Count = (int)response.PageInfo.TotalResults,
                Next = response.NextPageToken,
                Previous = response.PrevPageToken,
                Results = response.Items
						    .Where(itm => itm.Id.Kind == "youtube#video")
						    .Select(BuildVideoModel)
            };
	    }

        private YouTubeVideoListItem BuildVideoModel(SearchResult result) 
	    { 
            return new YouTubeVideoListItem
			{
			    Id = result.Id.VideoId,
			    Title = result.Snippet.Title,
			    Description = result.Snippet.Description,
                Uri = "https://www.youtube.com/watch?v=" + result.Id.VideoId,
                ImageUri = result.Snippet.Thumbnails.Default__.Url,
			};
    	}
    }
}
