using System;
using System.Collections.Generic;

namespace MusicRoom.API.Models
{
    public class Query
    {
        public int Total { get; set; }

        public IEnumerable<Track> Tracks { get; set; }

        public IEnumerable<Track> Albums { get; set; }

        public IEnumerable<Track> Artists { get; set; }
    }
}
