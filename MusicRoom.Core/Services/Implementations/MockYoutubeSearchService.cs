using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.Core.Models;
using MusicRoom.Core.Services.Interfaces;

namespace MusicRoom.Core.Services.Implementations
{
    public class MockYoutubeSearchService : IYoutubeSearchService
    {
        public MockYoutubeSearchService()
        {
        }

        public Task<PagedResult<YouTubeVideo>> GetNextPageAsync(string token)
            => Task.FromResult(new PagedResult<YouTubeVideo>
            {
                Count = 100000,
                Next = "1234",
                Previous = string.Empty,
                Results = new List<YouTubeVideo>()
                {
                    new YouTubeVideo
                    {
                        Title = "The Perfect Girl - Mareux",
                        Description = "A test description",
                        Id = "V1l6kxQNq54",
                        Uri = "https://www.youtube.com/watch?v=V1l6kxQNq54",
                        ImageUri = "https://i.ytimg.com/vi/V1l6kxQNq54/hqdefault.jpg?sqp=-oaymwEjCOADEI4CSFryq4qpAxUIARUAAAAAGAElAADIQj0AgKJDeAE=&rs=AOn4CLBdI_PxctR8gl5Tl5Kg__WTfwU1yw"

                    },
                    new YouTubeVideo
                    {
                        Title = "Alberto Balsalm",
                        Description = "Provided to YouTube by PIAS Alberto Balsalm · Aphex Twin ... I Care Because You Do",
                        Id= "-ZVZgCrHy5E",
                        Uri = "https://www.youtube.com/watch?v=-ZVZgCrHy5E",
                        ImageUri ="https://i.ytimg.com/vi/-ZVZgCrHy5E/hq720.jpg?sqp=-oaymwEjCOgCEMoBSFryq4qpAxUIARUAAAAAGAElAADIQj0AgKJDeAE=&rs=AOn4CLADTJC1RU4jro74QIzs5aBFAS0aHA"
                    },
                    new YouTubeVideo
                    {
                        Title = "Joe Mama",
                        Description = "",
                        Id = "bfOcF7Zeu6I",
                        Uri = "https://www.youtube.com/watch?v=bfOcF7Zeu6I",
                        ImageUri = "https://i.ytimg.com/vi/bfOcF7Zeu6I/hqdefault.jpg?sqp=-oaymwEjCOADEI4CSFryq4qpAxUIARUAAAAAGAElAADIQj0AgKJDeAE=&rs=AOn4CLABtpbWPt_wJtitZzOYnFjGgVDukw",
                    },

                }
            });
        public Task<PagedResult<YouTubeVideo>> SearchVideosAsync(string query) 
            => Task.FromResult(new PagedResult<YouTubeVideo>
            {
                Count = 100000,
                Next = "1234",
                Previous = string.Empty,
                Results = new List<YouTubeVideo>()
                {
                    new YouTubeVideo
                    {
                        Title = "Spongebob Squarepants - Rev Up Those Fryers",
                        Description = "No shirt No shoes No service",
                        Id = "2L0G2G4V3T4",
                        Uri = "https://www.youtube.com/watch?v=2L0G2G4V3T4",
                        ImageUri = "https://i.ytimg.com/vi/2L0G2G4V3T4/hqdefault.jpg?sqp=-oaymwEjCOADEI4CSFryq4qpAxUIARUAAAAAGAElAADIQj0AgKJDeAE=&rs=AOn4CLAA56Y11XNSqj6neOxaZKXbpwHn1g"

                    },
                    new YouTubeVideo
                    {
                        Title = "Spongebob I Heart Dancing",
                        Description = "",
                        Id= "-ZVZgCrHy5E",
                        Uri = "https://www.youtube.com/watch?v=up2JZwy18eY",
                        ImageUri ="https://i.ytimg.com/vi/up2JZwy18eY/hqdefault.jpg?sqp=-oaymwEjCOADEI4CSFryq4qpAxUIARUAAAAAGAElAADIQj0AgKJDeAE=&rs=AOn4CLAIZNG5DX6mo_sHZSbv3rfAjVfv0A"
                    },
                    new YouTubeVideo
                    {
                        Title = "Have you finished those errands?",
                        Description = "This is reuploaded because I got terminated in my old youtube channel",
                        Id = "jKnSVLaSsZI",
                        Uri = "https://www.youtube.com/watch?v=jKnSVLaSsZI",
                        ImageUri = "https://i.ytimg.com/vi/jKnSVLaSsZI/hq720.jpg?sqp=-oaymwEjCOgCEMoBSFryq4qpAxUIARUAAAAAGAElAADIQj0AgKJDeAE=&rs=AOn4CLDbSKRQ7stVqhO_nYvy5aAsRIAsE…g",
                    },

                }
            });
    }
}
