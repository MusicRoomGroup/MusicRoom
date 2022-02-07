using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MusicRoom.API.APIs;
using MusicRoom.API.Enums;
using MusicRoom.API.Interfaces;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Xamarin.Essentials;

namespace MusicRoom.API.Factories
{
    public class MusicAPIFactory : IAPIFactory
    {
        private static EmbedIOAuthServer _server;

        private SpotifyClient _spotify;

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
            await AuthorizeSpotifyAsync();

    	    return await Task.FromResult(new SpotifyPlayerAPI(_spotify));
        }

        private async Task AuthorizeSpotifyAsync()
	    {
			_server = new EmbedIOAuthServer(new Uri("http://localhost:5000/callback"), 5000); 

            await _server.Start();

            _server.ImplictGrantReceived += OnImplicitGrantReceivedAsync;

            _server.ErrorReceived += OnErrorReceivedAsync;

            var request = new LoginRequest(_server.BaseUri, "e6320f6b0830403ebe516c21b852a02a", LoginRequest.ResponseType.Token)
            {
                Scope = new List<string> 
		        { 
		            Scopes.AppRemoteControl, 
		            Scopes.UserReadPrivate,
                    Scopes.UserReadPlaybackState,
                    Scopes.UserModifyPlaybackState
		        }
            };

            try
            { 
			    await Browser.OpenAsync(request.ToUri(), BrowserLaunchMode.External);
                //await Browser.OpenAsync(request.ToUri());
            }
            catch (NotImplementedInReferenceAssemblyException)
            {
			    BrowserUtil.Open(request.ToUri());
	        }



            _event.WaitOne();
        }

        private async Task OnImplicitGrantReceivedAsync(object sender, ImplictGrantResponse response)
        {
            await _server.Stop();
            _spotify = new SpotifyClient(response.AccessToken);
            _event.Set();
        }

        private static async Task OnErrorReceivedAsync(object sender, string error, string state)
        {
            await _server.Stop();

            throw new NotSupportedException($"Aborting authorization, error received: {error}");
        }
    }
}
