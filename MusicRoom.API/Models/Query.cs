using System;
using System.Collections.Generic;
using SpotifyAPI.Web.Models;

namespace MusicRoom.API.Models
{
    public class Query
    {
        public int Total { get; set; }

        public IEnumerable<Track> Tracks { get; set; }

        public Paging<Track> Albums { get; set; }

        public Paging<Track> Artists { get; set; }
    }
}
