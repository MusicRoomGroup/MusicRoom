using System.Collections.Generic;

namespace MusicRoom.Core.Models
{
    public class PagedResult<T>
    {
        public int Count { get; set; }

        public string Next { get; set; }

        public string Previous { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}
