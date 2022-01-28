using System;
using System.Collections.Generic;
using System.Linq;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;

namespace MusicRoom.TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            ImplicitGrantAuth auth = new(
               "e6320f6b0830403ebe516c21b852a02a",
               "http://localhost:4002",
               "http://localhost:4002",
               Scope.UserReadPlaybackState |
               Scope.UserReadPrivate |
               Scope.UserModifyPlaybackState 
            );

            auth.AuthReceived += async (sender, payload) => 
	        {
                auth.Stop();
			    SpotifyWebAPI spotify = new()
			    {
					AccessToken = payload.AccessToken,
					TokenType = payload.TokenType,
			    };

                var tracks = await spotify.SearchItemsAsync("mareux+the+perfect+girl", SearchType.Track);
                var track = tracks.Tracks.Items.First();

                var devices = await spotify.GetDevicesAsync();
                var tracksToPlay = new List<string> { track.Uri };
                await spotify.ResumePlaybackAsync(uris: tracksToPlay, offset: 0);
	        };

		    auth.Start();

            auth.OpenBrowser();

            Console.ReadLine();
        }
    }
}
