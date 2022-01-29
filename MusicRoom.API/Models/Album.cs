using System;
using System.Collections.Generic;

namespace MusicRoom.API.Models
{
    public class Album : Item
    {
        public int TotalTracks { get; set; }

        public IEnumerable<string> Artists { get; set; }
    }
}
