using System;
using System.Collections.Generic;

namespace MusicRoom.API.Models
{
    public class Track : Item
    {
        public string AlbumName { get; set; }

        public string  Artists { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
