using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.API.Interfaces;
using MusicRoom.API.Models;

namespace MusicRoom.API.APIs
{
    public class SoundCloudPlayerAPI : IPlayerAPI
    {
        public SoundCloudPlayerAPI()
        {
        }

        public Task<Album> GetAlbumAsync(string albumId) => throw new NotImplementedException();

        public Task<Artist> GetArtistAsync(string artistId) => throw new NotImplementedException();

        public Task<IEnumerable<Device>> GetDevicesAsync() => throw new NotImplementedException();

        public Task<Track> GetTrackAsync(string trackId) => throw new NotImplementedException();

        public Task PlaySong(string uri) => throw new NotImplementedException();

        public Task PlaySong(string deviceId, string uri) => throw new NotImplementedException();

        public Task PlaySongs(IEnumerable<string> uris) => throw new NotImplementedException();

        public Task<IEnumerable<Album>> SearchAlbumsAsync(string query) => throw new NotImplementedException();

        public Task<IEnumerable<Artist>> SearchArtistsAsync(string query) => throw new NotImplementedException();

        public Task<Query> SearchAsync(string query) => throw new NotImplementedException();

        public Task<IEnumerable<Track>> SearchTracksAsync(string query) => throw new NotImplementedException();

        public Task SelectDeviceAsync(string deviceId) => throw new NotImplementedException();
    }
}
