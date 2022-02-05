using System;
using System.Linq;
using System.Threading.Tasks;
using MusicRoom.Core.Models;
using MusicRoom.Core.Services.Interfaces;
using YoutubeExplode;
using YoutubeExplode.Search;
using YoutubeExplode.Common;
using System.Collections.Generic;

namespace MusicRoom.Core.Services.Implementations
{
    public class YouTubeSearchService : IYoutubeSearchService
    {
		private static List<PagedResult<YouTubeVideoListItem>> _cachedResults { get; set; }

        private readonly YoutubeClient _youtube;
        private readonly int _limit = 20;

        
        public YouTubeSearchService()
        {
            _youtube = new YoutubeClient();
            _cachedResults = new List<PagedResult<YouTubeVideoListItem>>();
        }

        public Task<PagedResult<YouTubeVideoListItem>> GetNextPageAsync(string token)
        {
            return int.TryParse(token, out var index)
                ? Task.FromResult(_cachedResults[index])
                : throw new KeyNotFoundException("The page does not exist");
        }

        public async Task<PagedResult<YouTubeVideoListItem>> SearchVideosAsync(string query) 
        {
            // TODO: consider not awaiting the cache building process and just return the first result
            await BuildCacheAsync(query);

            return _cachedResults.First();
	    }

        private async Task BuildCacheAsync(string query) 
	    {
            _cachedResults = new List<PagedResult<YouTubeVideoListItem>>();

			

            var pageCount = 0;

            await foreach(Batch<ISearchResult> result in _youtube.Search.GetResultBatchesAsync(query))
            {
                pageCount++;
                var pagedResult = new PagedResult<YouTubeVideoListItem>();
                pagedResult.Count = result.Items.Count();
				pagedResult.Results = result.Items.OfType<VideoSearchResult>().Select(BuildYouTubeListItem);
                pagedResult.Next = pageCount.ToString();
                _cachedResults.Add(pagedResult);
                if (pageCount > 0)
                {
                    pagedResult.Previous = (pageCount - 1).ToString();
                }
                if (pageCount >= _limit)
                {
                    pagedResult.Next = null;
                    break;
                }
                
	        }
	
	    }
        private YouTubeVideoListItem BuildYouTubeListItem(VideoSearchResult videoResult)
		{
			return new YouTubeVideoListItem
			{
				Author = videoResult.Author.Title,
				Id = videoResult.Id,
				ImageUri = videoResult.Thumbnails.First().Url,
				Title = videoResult.Title,
				Uri = videoResult.Url,
				Duration = (TimeSpan)videoResult.Duration
			};
		} 
    }
}
