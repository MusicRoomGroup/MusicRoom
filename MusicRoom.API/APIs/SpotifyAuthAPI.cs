using System;
using System.Threading.Tasks;
using MusicRoom.API.Interfaces;

namespace MusicRoom.API.APIs
{
    public class SpotifyAuthAPI : IAuthAPI
    {
        public SpotifyAuthAPI()
        {
        }

        public Task<bool> Authorize() => throw new NotImplementedException();
    }
}
