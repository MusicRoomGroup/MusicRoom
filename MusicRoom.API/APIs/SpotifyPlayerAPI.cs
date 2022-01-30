using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicRoom.API.Interfaces;
using MusicRoom.API.Models;
using SpotifyAPI.Web;

namespace MusicRoom.API.APIs
{
    public class SpotifyPlayerAPI : IPlayerAPI
    {
        private static SpotifyClient _spotify;

        public SpotifyPlayerAPI(SpotifyClient spotify)
        {
            _spotify = spotify;
        }

        public async Task<Track> GetTrackAsync(string trackId)
            => BuildTrack(await _spotify.Tracks.Get(trackId));

        public async Task<Album> GetAlbumAsync(string albumId)
            => BuildAlbum(await _spotify.Albums.Get(albumId));

        public async Task<Artist> GetArtistAsync(string artistId) 
            => BuildArtist(await _spotify.Artists.Get(artistId));

        public async Task<Query> SearchAsync(string query)
        {
            var request = new SearchRequest(SearchRequest.Types.All, query);
            return BuildQuery(await _spotify.Search.Item(request));
	    }

        public async Task<IEnumerable<Track>> SearchTracksAsync(string query)
        {
            var request = new SearchRequest(SearchRequest.Types.Track, query);
            SearchResponse response = await _spotify.Search.Item(request);
            return response.Tracks.Items.Select(BuildTrack);
        }

        public async Task<IEnumerable<Album>> SearchAlbumsAsync(string query)
        {
            var request = new SearchRequest(SearchRequest.Types.Album, query);
            SearchResponse response = await _spotify.Search.Item(request);
            return response.Albums.Items.Select(BuildAlbum);
        }

        public async Task<IEnumerable<Artist>> SearchArtistsAsync(string query)
        {
            var request = new SearchRequest(SearchRequest.Types.Artist, query);
            SearchResponse response = await _spotify.Search.Item(request);
            return response.Artists.Items.Select(BuildArtist);
        }

        public async Task<IEnumerable<Models.Device>> GetDevicesAsync()
        {

            DeviceResponse availableDevices = await _spotify.Player.GetAvailableDevices();

            return availableDevices.Devices
            .Select(d => new Models.Device()
            {
                Id = d.Id,
                Name = d.Name,
                Active = d.IsActive
            });
        }

        public async Task PlaySongs(IEnumerable<string> uris)
        { 
            var request = new PlayerResumePlaybackRequest() 
	        { 
		        Uris = uris.ToList(),
	        };
            await _spotify.Player.ResumePlayback(request);
	    }

        public async Task PlaySong(string uri)
        {
            var request = new PlayerResumePlaybackRequest() { Uris = new List<string>() { uri } };
            await _spotify.Player.ResumePlayback(request);
	    }

        public async Task PlaySong(string deviceId, string uri)
        { 
            var request = new PlayerResumePlaybackRequest() 
	        { 
                DeviceId = deviceId,
		        Uris = new List<string>() { uri },
	        };
            await _spotify.Player.ResumePlayback(request);
	    }

        public async Task SelectDeviceAsync(string deviceId)
        { 
            await _spotify.Player.TransferPlayback(new PlayerTransferPlaybackRequest(new List<string>() { deviceId }));
	    }

        #region private methods
        private static Track BuildTrack(FullTrack fullTrack)
        {
            return new Track()
            {
                Id = fullTrack.Id,
                Name = fullTrack.Name,
                Uri = fullTrack.Uri,
                AlbumName = fullTrack.Album.Name,
                ImageUrl = new Uri(fullTrack.Album.Images.First().Url),
                Artists = string.Join(", ", fullTrack.Artists.Select(a => a.Name)),
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
                ImageUrl = new Uri(album.Images.First().Url),
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
                ImageUrl = new Uri(album.Images.First().Url),
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
                ImageUrl = new Uri(artist.Images.First().Url),
                Genres = artist.Genres
            };
        }

        private static Query BuildQuery(SearchResponse item)
        {
            return new Query
            {
                Tracks = item.Tracks.Items.Select(BuildTrack),
            };
        }
        #endregion
    }
}
