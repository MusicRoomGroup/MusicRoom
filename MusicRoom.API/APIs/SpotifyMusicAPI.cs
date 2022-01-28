using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.API.Interfaces;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace MusicRoom.API.MusicAPIs
{
    public class SpotifyMusicAPI : IMusicAPI
    {
        private static SpotifyWebAPI _spotify;

        public SpotifyMusicAPI() { }

        public async Task Initialize() 
    	{
            ImplicitGrantAuth auth = new(
               "e6320f6b0830403ebe516c21b852a02a",
               "http://localhost:4002",
               "http://localhost:4002",
               Scope.UserReadPrivate
	        );

            auth.AuthReceived += (sender, payload) => 
	        {
                auth.Stop();
			    _spotify = new()
			    {
					AccessToken = payload.AccessToken,
					TokenType = payload.TokenType,
			    };
	        };

		    auth.Start();
            auth.OpenBrowser();
	    }

        public async Task<FullTrack> GetTrack(string trackName)
        {
            return await _spotify.GetTrackAsync(trackName);
        }

        public async Task<SearchItem> Search(string query)
        {
            return await _spotify.SearchItemsAsync(query, SearchType.All);
        }

        public async Task<SearchItem> SearchAlbum(string query)
        {
            return await _spotify.SearchItemsAsync(query, SearchType.Album);
        }

		public async Task<IEnumerable<FullTrack>> SearchTracks(string query)
        {
            var items = await _spotify.SearchItemsAsync(query, SearchType.Track);

            return items.Tracks.Items;
        }

        public async Task PlaySong(string uri)
        {
            var profile = _spotify.GetPrivateProfile();

            Console.WriteLine(profile.Email); 

            AvailabeDevices devices = await _spotify.GetDevicesAsync();

            //devices.Devices?.ForEach(device => Console.WriteLine(device.Name));
        }
    }
}
