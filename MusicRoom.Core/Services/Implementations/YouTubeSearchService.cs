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
        private static IAsyncEnumerator<Batch<ISearchResult>> _enumerator;

        private static Queue<PagedResult<YouTubeVideoListItem>> _queueCache;

		private readonly int _limit = 20;

        private readonly YoutubeClient _youtube;

		private bool _isQueuing = false;

        public YouTubeSearchService()
        {
            _youtube = new YoutubeClient();

			_queueCache = new Queue<PagedResult<YouTubeVideoListItem>>();
        }

        public async Task<PagedResult<YouTubeVideoListItem>> GetNextPageAsync(string token)
        {
            if (_queueCache.TryDequeue(out PagedResult<YouTubeVideoListItem> result)) { 
				return result;
			} else if (_isQueuing) {
                await Task.Delay(10);
                return await GetNextPageAsync(token);
            } else {
                await _enumerator.MoveNextAsync();
                return BuildPagedResult(_enumerator.Current.Items);
            }
        }

        public async Task<PagedResult<YouTubeVideoListItem>> SearchVideosAsync(string query) 
        {
             _enumerator = _youtube.Search.GetResultBatchesAsync(query).GetAsyncEnumerator();

			await _enumerator.MoveNextAsync();

		    Batch<ISearchResult> firstPage = _enumerator.Current;

		    _ = Task.Run(() => CacheResultsAsync());

			return BuildPagedResult(firstPage.Items);
	    }

		private async Task CacheResultsAsync()
		{
			_isQueuing = true;
			var pageCount = 0;
			while ( await _enumerator.MoveNextAsync())
			{
				_queueCache.Enqueue(BuildPagedResult(_enumerator.Current.Items));
				pageCount++; 
				if (pageCount > _limit)
				{
					break;
				}
			}
			_isQueuing = false;
			;
		}

		private PagedResult<YouTubeVideoListItem> BuildPagedResult(IReadOnlyList<ISearchResult> results)
        {
            return new PagedResult<YouTubeVideoListItem>
			{
				Count = results.Count,
				Results = results.OfType<VideoSearchResult>().Select(BuildYouTubeListItem),
			};
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
				Duration = videoResult.Duration
			};
		} 
    }
}
