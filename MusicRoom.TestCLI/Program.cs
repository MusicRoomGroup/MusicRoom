using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicRoom.API.Enums;
using MusicRoom.API.Factories;
using MusicRoom.API.Interfaces;
using MusicRoom.API.Models;

namespace MusicRoom.API.Tests.CLI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IAPIFactory factory = new MusicAPIFactory(SupportedAPI.Spotify);

            IPlayerAPI player = await factory.BuildPlayerAPIAsync();

            IEnumerable<Device> devices = await player.GetDevicesAsync();

            if (devices.Any())
            { 
                PagedResult<Track> tracks = await player.SearchTracksAsync("the+perfect+girl+mareux");

                Track track = tracks.Results.First();

                Console.WriteLine(track.ImageUrl);

                Device device = devices.First();

                await player.PlaySong(device.Id, track.Uri);

                Console.ReadLine();
            }
        }
    }
}
