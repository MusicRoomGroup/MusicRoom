using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.API.Models;

namespace MusicRoom.API.Interfaces
{
    public interface ISearchAPI
    {
        Task<Track> GetTrackAsync(string trackId);

        Task<Album> GetAlbumAsync(string albumId);

        Task<Artist> GetArtistAsync(string artistId);

        Task<Query> SearchAsync(string query);

        Task<IEnumerable<Track>> SearchTracksAsync(string query);

        Task<IEnumerable<Album>> SearchAlbumsAsync(string query);

        Task<IEnumerable<Artist>> SearchArtistsAsync(string query);
    }
}
