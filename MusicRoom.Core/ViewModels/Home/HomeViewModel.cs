using System.Threading.Tasks;
using MusicRoom.Core.Models;
using MusicRoom.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

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

        private MvxObservableCollection<YouTubeVideo> _videoList;
        public MvxObservableCollection<YouTubeVideo> VideoList
        {
            get => _videoList;
            set
            {
                _videoList = value;
                RaisePropertyChanged(() => VideoList);
            }
        }

        private PagedResult<YouTubeVideo> _videoPage;
        public PagedResult<YouTubeVideo> VideoPage
        {
            get => _videoPage;
            set
            {
                _videoPage = value;
                RaisePropertyChanged(() => VideoPage);
            }
        }

        private YouTubeVideo _video;
        public YouTubeVideo Video
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

        public IMvxCommand<YouTubeVideo> PlayVideoAsyncCommand
            => new MvxAsyncCommand<YouTubeVideo>(PlayAsync);

        private async Task SearchAsync()
        {
            VideoPage = await _youtube.SearchVideosAsync(VideoQuery);

            VideoList = new MvxObservableCollection<YouTubeVideo>(VideoPage.Results);
	    }

        private Task PlayAsync(YouTubeVideo video)
        {
            //TODO: implement music player logic
            return Task.CompletedTask;
	    }
    }
}
