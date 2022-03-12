using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.API.Models;

namespace MusicRoom.API.Interfaces
{
    public interface IDeviceApi
    {
        Task<IEnumerable<Device>> GetDevicesAsync();

        Task SelectDeviceAsync(string deviceId);

        Task PlaySong(string uri);

        Task PlaySong(string deviceId, string uri);

        Task PlaySongs(IEnumerable<string> uris);
    }
}
