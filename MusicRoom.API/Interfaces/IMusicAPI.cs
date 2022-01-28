using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyAPI.Web.Models;

namespace MusicRoom.API.Interfaces
{
    public interface IMusicAPI
    {
        Task Initialize();

        Task<FullTrack> GetTrack(string trackName);

        Task<SearchItem> Search(string query);

        Task<IEnumerable<FullTrack>> SearchTracks(string query);

        Task<SearchItem> SearchAlbum(string query);

        Task PlaySong(string uri);
    }
}
