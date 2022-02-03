using System;
using System.Threading.Tasks;
using MusicRoom.Core.Models;

namespace MusicRoom.Core.Services.Interfaces
{
    public interface IYoutubeSearchService
    {
        Task<PagedResult<YouTubeVideo>> SearchVideosAsync(string query);

        Task<PagedResult<YouTubeVideo>> GetNextPageAsync(string token);
    }
}
