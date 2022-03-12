using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.API.Models;

namespace MusicRoom.API.Interfaces
{
    public interface ISearchApi
    {
        Task<Track> GetTrackAsync(string trackId);

        Task<Album> GetAlbumAsync(string albumId);

        Task<Artist> GetArtistAsync(string artistId);

        Task<Query> SearchAsync(string query);

        Task<PagedResult<Track>> SearchTracksAsync(string query);

        Task<PagedResult<Album>> SearchAlbumsAsync(string query);

        Task<PagedResult<Artist>> SearchArtistsAsync(string query);
    }
}
