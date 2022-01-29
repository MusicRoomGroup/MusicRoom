using System;
using System.Threading;
using System.Threading.Tasks;
using MusicRoom.API.Enums;
using MusicRoom.API.Interfaces;
using MusicRoom.API.MusicAPIs;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;

namespace MusicRoom.API.Factories
{
    public class MusicAPIFactory : IAPIFactory
    {
        private SpotifyWebAPI _spotify;

        private readonly SupportedAPI _api;

        private readonly AutoResetEvent _event = new AutoResetEvent(false);

        public MusicAPIFactory() 
        {
            _api = SupportedAPI.Spotify;
        }

        public MusicAPIFactory(SupportedAPI api) 
        {
            _api = api;
        }

        public async Task<IPlayerAPI> BuildPlayerAPIAsync() => _api switch
        {
            SupportedAPI.Spotify => await BuildSpotifyPlayerAPIAsync(),
            _ => throw new NotImplementedException(),
        };

        private async Task<IPlayerAPI> BuildSpotifyPlayerAPIAsync()
        {
            AuthorizeSpotify();

            _event.WaitOne();

    	    return await Task.FromResult(new SpotifyPlayerAPI(_spotify));
        }

        private void AuthorizeSpotify()
	    {
            var auth = new ImplicitGrantAuth(
               "e6320f6b0830403ebe516c21b852a02a", // TODO: replace with server call to get the client id
               "http://localhost:4002",
               "http://localhost:4002",
               Scope.UserReadPlaybackState |
               Scope.UserReadPrivate |
               Scope.UserModifyPlaybackState 
            );

            auth.AuthReceived += (sender, payload) => 
	        {
                auth.Stop();
                _spotify = new SpotifyWebAPI()
			    {
					AccessToken = payload.AccessToken,
					TokenType = payload.TokenType,
			    };

                _event.Set();
	        };

		    auth.Start();

            auth.OpenBrowser();
        }
    }
}
