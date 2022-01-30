using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MusicRoom.API.Interfaces;
using MusicRoom.API.Models;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace MusicRoom.API.MusicAPIs
{
    public class SpotifyPlayerAPI : IPlayerAPI
    {
        private static SpotifyWebAPI _spotify;

        public SpotifyPlayerAPI(SpotifyWebAPI spotify)
        {
            _spotify = spotify;
        }

        public async Task<Track> GetTrackAsync(string trackId)
            => BuildTrack(await _spotify.GetTrackAsync(trackId));

        public async Task<Album> GetAlbumAsync(string albumId)
            => BuildAlbum(await _spotify.GetAlbumAsync(albumId));

        public async Task<Artist> GetArtistAsync(string artistId) 
            => BuildArtist(await _spotify.GetArtistAsync(artistId));

        public async Task<Query> SearchAsync(string query)
            => BuildQuery(await _spotify.SearchItemsAsync(query, SearchType.All));

        public async Task<IEnumerable<Track>> SearchTracksAsync(string query)
        {
            SearchItem search = await _spotify.SearchItemsAsync(query, SearchType.Track);

            return search.Tracks.Items.Select(BuildTrack);
        }

        public async Task<IEnumerable<Album>> SearchAlbumsAsync(string query)
        {
            SearchItem search = await _spotify.SearchItemsAsync(query, SearchType.Album);

            return search.Albums.Items.Select(BuildAlbum);
        }

        public async Task<IEnumerable<Artist>> SearchArtistsAsync(string query)
        {
            SearchItem search = await _spotify.SearchItemsAsync(query, SearchType.Artist);

            return search.Artists.Items.Select(BuildArtist);
        }

        public async Task<IEnumerable<Models.Device>> GetDevicesAsync()
        {
            
            AvailabeDevices availableDevices = await _spotify.GetDevicesAsync();

            return availableDevices.Devices
            .Select(d => new Models.Device()
            {
                Id = d.Id,
                Name = d.Name,
                Active = d.IsActive
            });
        }

        public async Task PlaySongs(IEnumerable<string> uris)
            => await _spotify.ResumePlaybackAsync(uris: uris.ToList(), offset: 0);

        public async Task PlaySong(string uri)
            => await _spotify.ResumePlaybackAsync(uris: new List<string>() { uri }, offset: 0);

        public async Task PlaySong(string deviceId, string uri) 
		    => await _spotify.ResumePlaybackAsync(deviceId: deviceId, uris: new List<string>() { uri }, offset: 0);

        public async Task SelectDeviceAsync(string deviceId)
            => await _spotify.TransferPlaybackAsync(deviceId);

        #region private methods
        private static Track BuildTrack(FullTrack fullTrack)
        {
            return new Track()
            {
                Id = fullTrack.Id,
                Name = fullTrack.Name,
                Uri = fullTrack.Uri,
                AlbumName = fullTrack.Album.Name,
                ImageUrl = fullTrack.Album.Images.First().Url,
                Artists = fullTrack.Artists.Select(a => a.Name ),
                Duration = TimeSpan.FromMilliseconds(fullTrack.DurationMs)
            };
        }

        private static Album BuildAlbum(SimpleAlbum album)
        {
            return new Album()
            {
                Id = album.Id,
                Name = album.Name,
                Uri = album.Uri,
                TotalTracks = album.TotalTracks,
                ImageUrl = album.Images.First().Url,
                Artists = album.Artists.Select(a => a.Name ),
            };
        }

        private static Album BuildAlbum(FullAlbum album)
        {
            return new Album()
            {
                Id = album.Id,
                Name = album.Name,
                Uri = album.Uri,
                TotalTracks = album.TotalTracks,
                ImageUrl = album.Images.First().Url,
                Artists = album.Artists.Select(a => a.Name),
                Genres = album.Genres
            };
        }

        private static Artist BuildArtist(SimpleArtist artist)
        {
            return new Artist()
            {
                Id = artist.Id,
                Name = artist.Name,
                Uri = artist.Uri
            };
        }

        private static Artist BuildArtist(FullArtist artist)
        {
            return new Artist()
            {
                Id = artist.Id,
                Name = artist.Name,
                ImageUrl = artist.Images.First().Url,
                Genres = artist.Genres
            };
        }

        private static Query BuildQuery(SearchItem item)
        {
            return new Query
            {
                Tracks = item.Tracks.Items.Select(BuildTrack),

            };
        }

        
        #endregion
    }
}
