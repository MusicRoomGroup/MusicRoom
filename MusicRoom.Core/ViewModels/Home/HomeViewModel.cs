using System.Linq;
using System.Threading.Tasks;
using MusicRoom.Core.Models;
using MusicRoom.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace MusicRoom.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private string _videoQuery;
        public string VideoQuery
        {
            get => _videoQuery;
            set
            {
                _videoQuery = value;
                RaisePropertyChanged(() => VideoQuery);
            }
        }

        private MvxObservableCollection<YouTubeVideoListItem> _videoList;
        public MvxObservableCollection<YouTubeVideoListItem> VideoList
        {
            get => _videoList;
            set
            {
                _videoList = value;
                RaisePropertyChanged(() => VideoList);
            }
        }

        private PagedResult<YouTubeVideoListItem> _videoPage;
        public PagedResult<YouTubeVideoListItem> VideoPage
        {
            get => _videoPage;
            set
            {
                _videoPage = value;
                Total += VideoPage.Results.Count();
                RaisePropertyChanged(() => VideoPage);
            }
        }

        private string _videoCount = "Search Videos";
        public string VideoCount
        {
            get => _videoCount;
            set
            {
                _videoCount = value;
                RaisePropertyChanged(() => VideoCount);
            }
        }

        private int _total;
        public int Total
        {
            get => _total;
            set
            {
                _total = value;
                if (VideoPage != null)
                { 
					VideoCount = $"Displaying {Total} out of {VideoPage.Count}";
		        }
                RaisePropertyChanged(() => Total);
            }
        }

        private YouTubeVideoListItem _video;
        public YouTubeVideoListItem Video
        {
            get => _video;
            set
            {
                _video = value;
                RaisePropertyChanged(() => Video);
            }
        }

        private readonly IYoutubeSearchService _youtube;

        public HomeViewModel(IYoutubeSearchService youtube)
        {
            _youtube = youtube;
        }

        public override async Task Initialize()
	    {
            await base.Initialize();
        }

        public IMvxCommand SearchAsyncCommand
            => new MvxAsyncCommand(SearchAsync);

        public IMvxCommand<YouTubeVideoListItem> PlayVideoAsyncCommand
            => new MvxAsyncCommand<YouTubeVideoListItem>(PlayAsync);

        public IMvxCommand GetNextPageCommand
            => new MvxAsyncCommand(GetNextPageAsync);

        private async Task SearchAsync()
        {
            Total = 0;

            VideoPage = await _youtube.SearchVideosAsync(VideoQuery);

            VideoList = new MvxObservableCollection<YouTubeVideoListItem>(VideoPage.Results);
	    }

        private async Task PlayAsync(YouTubeVideoListItem video)
        {
            await Browser.OpenAsync(video.Uri);
	    }

        private async Task GetNextPageAsync()
        {
            VideoPage = await _youtube.GetNextPageAsync(VideoPage.Next);

            VideoList.AddRange(VideoPage.Results);
	    }
    }
}
