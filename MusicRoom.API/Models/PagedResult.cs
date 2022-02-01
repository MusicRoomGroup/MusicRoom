using System.Collections.Generic;

namespace MusicRoom.API.Models
{
    public class PagedResult<T> where T : Item
    {
        public int Count { get; set; }

        public string Next { get; set; }

        public string Previous { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}
