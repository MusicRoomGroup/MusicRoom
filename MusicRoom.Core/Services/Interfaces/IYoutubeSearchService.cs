using System;
using System.Threading.Tasks;
using MusicRoom.Core.Models;

namespace MusicRoom.Core.Services.Interfaces
{
    public interface IYoutubeSearchService
    {
        Task<PagedResult<YouTubeVideoListItem>> SearchVideosAsync(string query);

        Task<PagedResult<YouTubeVideoListItem>> GetNextPageAsync(string token);
    }
}
