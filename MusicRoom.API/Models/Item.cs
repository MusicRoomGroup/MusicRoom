using System;
using System.Collections.Generic;

namespace MusicRoom.API.Models
{
    public abstract class Item
    {
	    public string Id { get; set; }

	    public string Name { get; set; }

	    public string ImageUrl { get; set; }

	    public string Uri { get; set; }

	    public IEnumerable<string> Genres { get; set; }
    }
}
