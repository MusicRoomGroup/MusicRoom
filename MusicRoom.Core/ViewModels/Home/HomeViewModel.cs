using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using DynamicData;
using MusicRoom.Core.Models;
using MusicRoom.Core.Services.Interfaces;
using ReactiveUI;
using Splat;

namespace MusicRoom.Core.ViewModels.Home
{
    public class HomeViewModel : ViewModelBase
    {
        public string Uri => "YouTubePlayer.html";

        private string _videoQuery;

        [DataMember]
        public string VideoQuery
        {
            get => _videoQuery;
            set => this.RaiseAndSetIfChanged(ref _videoQuery, value);
        }

        private ObservableCollection<YouTubeVideoListItem> _videoList;

        [DataMember]
        public ObservableCollection<YouTubeVideoListItem> VideoList
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
                .CreateFromTask( async () => await SearchAsync(),
                    this.WhenAnyValue( vm => vm.VideoQuery,
                        q => !string.IsNullOrEmpty(q)));

            GetNextPageAsyncCommand = ReactiveCommand.CreateFromTask(async () => await GetNextPageAsync());

            PlayVideoCommand = ReactiveCommand.Create<YouTubeVideoListItem>(Play);

            //GoToChatAsyncCommand = ReactiveCommand.CreateFromTask(async () => await _navMan.Navigate<ChatViewModel>());
        }

        // TODO: replace mvx interaction
        // private MvxInteraction<string> _interaction = new MvxInteraction<string>();
        // public IMvxInteraction<string> Interaction => _interaction;

        public ReactiveCommand<Unit, Unit> SearchAsyncCommand { get; }

        public ReactiveCommand<YouTubeVideoListItem, Unit> PlayVideoCommand { get; }

        public ReactiveCommand<Unit, Unit> GetNextPageAsyncCommand { get; }

        public ReactiveCommand<Unit, Unit> GoToChatAsyncCommand { get; }

        private async Task SearchAsync()
        {
            Total = 0;
            IsLoading = true;

            VideoPage = await _youtube.SearchVideosAsync(VideoQuery);

            VideoList = new ObservableCollection<YouTubeVideoListItem>(VideoPage.Results);

            IsLoading = false;
	    }

        private void Play(YouTubeVideoListItem video)
        {
            // TODO: replace mvx interaction
			// _interaction.Raise(Video.Id);
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
