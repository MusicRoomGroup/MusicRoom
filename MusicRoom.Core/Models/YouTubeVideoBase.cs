using System;
namespace MusicRoom.Core.Models
{
    public abstract class YouTubeVideoBase
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public TimeSpan Duration { get; set; }

        public string Uri { get; set; }

        public string ImageUri { get; set; }
    }
}
