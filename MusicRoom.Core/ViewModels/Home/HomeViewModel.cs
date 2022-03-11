using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MusicRoom.Core.Models;
using MusicRoom.Core.Services.Interfaces;
using MvvmCross.ViewModels;
using ReactiveUI;
using Splat;

namespace MusicRoom.Core.ViewModels.Home
{
    public class HomeViewModel : ViewModelBase
    {
        public string Uri { get; } = "YouTubePlayer.html";

        private string _videoQuery;

        [DataMember]
        public string VideoQuery
        {
            get => _videoQuery;
            set => this.RaiseAndSetIfChanged(ref _videoQuery, value);
        }

        private MvxObservableCollection<YouTubeVideoListItem> _videoList;

        [DataMember]
        public MvxObservableCollection<YouTubeVideoListItem> VideoList
        {
            get => _videoList;
            set => this.RaiseAndSetIfChanged(ref _videoList, value);
        }

        private PagedResult<YouTubeVideoListItem> _videoPage;

        [DataMember]
        public PagedResult<YouTubeVideoListItem> VideoPage
        {
            get => _videoPage;
            set
            {
                Total += value.Results.Count();
                this.RaiseAndSetIfChanged(ref _videoPage, value);
            }
        }

        private ObservableAsPropertyHelper<string> _videoCount;
        public string VideoCount => _videoCount?.Value ?? "Search Videos";

        private int _total;

        [DataMember]
        public int Total
        {
            get => _total;
            set => this.RaiseAndSetIfChanged(ref _total, value);
        }

        private bool _isLoading;

        [DataMember]
        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        private YouTubeVideoListItem _video;

        [DataMember]
        public YouTubeVideoListItem Video
        {
            get => _video;
            set => this.RaiseAndSetIfChanged(ref _video, value);
        }

        private readonly IYoutubeSearchService _youtube;

        public HomeViewModel(IScreen screen = null, IYoutubeSearchService youtube = null) : base(screen)
        {
            UrlPathSegment = "Home";

            _youtube = youtube ?? Locator.Current.GetService<IYoutubeSearchService>();

            this
                .WhenAnyValue(
                    vm => vm.Total, _ => _,
                    (t1, _) => $"Displaying {t1} Videos")
                .ToProperty<HomeViewModel, string>(this, nameof(VideoCount));

            SearchAsyncCommand = ReactiveCommand
            .CreateFromTask(
                async () => await SearchAsync());
                //this.WhenAnyValue( vm => vm.VideoQuery));

            GetNextPageAsyncCommand = ReactiveCommand.CreateFromTask(async () => await GetNextPageAsync());

            PlayVideoCommand = ReactiveCommand.Create<YouTubeVideoListItem>(Play);

            //GoToChatAsyncCommand = ReactiveCommand.CreateFromTask(async () => await _navMan.Navigate<ChatViewModel>());
        }

        private MvxInteraction<string> _interaction = new MvxInteraction<string>();
        public IMvxInteraction<string> Interaction => _interaction;

        public readonly ReactiveCommand<Unit, Unit> SearchAsyncCommand;

        public ReactiveCommand<YouTubeVideoListItem, Unit> PlayVideoCommand;

        public readonly ReactiveCommand<Unit, Unit> GetNextPageAsyncCommand;

        public ReactiveCommand<Unit, Unit> GoToChatAsyncCommand;

        private async Task SearchAsync()
        {
            Total = 0;
            IsLoading = true;

            VideoPage = await _youtube.SearchVideosAsync(VideoQuery);

            VideoList = new MvxObservableCollection<YouTubeVideoListItem>(VideoPage.Results);

            IsLoading = false;
	    }

        private void Play(YouTubeVideoListItem video)
        {
			_interaction.Raise(Video.Id);
	    }

        private async Task GetNextPageAsync()
        {
            if (!IsLoading)
            {
			    IsLoading = true;

			    VideoPage = await _youtube.GetNextPageAsync(VideoPage.Next);

			    VideoList.AddRange(VideoPage.Results);

			    IsLoading = false;
			}
	    }
    }
}
