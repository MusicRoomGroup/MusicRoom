using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
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
        [DataMember] public string VideoQuery
        {
            get => _videoQuery;
            set => this.RaiseAndSetIfChanged(ref _videoQuery, value);
        }

        private ObservableCollection<YouTubeVideoListItem> _videoList;
        [DataMember] public ObservableCollection<YouTubeVideoListItem> VideoList
        {
            get => _videoList;
            set => this.RaiseAndSetIfChanged(ref _videoList, value);
        }

        private PagedResult<YouTubeVideoListItem> _videoPage;
        [DataMember] public PagedResult<YouTubeVideoListItem> VideoPage
        {
            get => _videoPage;
            set => this.RaiseAndSetIfChanged(ref _videoPage, value);
        }

        private readonly ObservableAsPropertyHelper<int> _count;
        [DataMember] public int Total => _count?.Value ?? 0;
        [DataMember] public string VideoCount => (_count?.Value ?? 0) > 0
            ? $"Displaying {_count?.Value} Videos"
            : "Search Videos";

        private bool _isLoading;
        [DataMember] public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        private YouTubeVideoListItem _video;
        [DataMember] public YouTubeVideoListItem Video
        {
            get => _video;
            set => this.RaiseAndSetIfChanged(ref _video, value);
        }

        private readonly IYoutubeSearchService _youtube;

        public HomeViewModel(IScreen screen = null, IYoutubeSearchService youtube = null) : base(screen)
        {
            UrlPathSegment = "Home";

            _youtube = youtube ?? Locator.Current.GetService<IYoutubeSearchService>();

            SearchAsyncCommand = ReactiveCommand.CreateFromTask( SearchAsync, this.WhenAnyValue(
                    vm => vm.VideoQuery,
                        q => !string.IsNullOrEmpty(q)));

            GetNextPageAsyncCommand = ReactiveCommand.CreateFromTask(GetNextPageAsync);

            PlayVideoCommand = ReactiveCommand.Create<YouTubeVideoListItem>(Play);

            // TODO: Replace mvx navigation with reactive ui
            //GoToChatAsyncCommand = ReactiveCommand.CreateFromTask(async () => await _navMan.Navigate<ChatViewModel>());

            // accumulate total videos
            _count = this
                .WhenAnyValue(vm => vm.VideoPage)
                .Where( vp => vp != null)
                .Select( vp => vp.Count + Total)
                .ToProperty(this, nameof(Total), Total, true);

            // notify video count property total changed
             this
                 .WhenAnyValue(vm => vm.Total)
                 .Where(t => t > 0)
                 .ToProperty(this, nameof(VideoCount));

            // any time a video changes we play the video command
            this
                .WhenAnyValue(v => v.Video)
                .Where( v => v != null)
                .InvokeCommand(PlayVideoCommand);
        }

        // TODO: replace mvx interaction with reactive ui
        // private MvxInteraction<string> _interaction = new MvxInteraction<string>();
        // public IMvxInteraction<string> Interaction => _interaction;

        public ReactiveCommand<Unit, Unit> SearchAsyncCommand { get; }

        public ReactiveCommand<YouTubeVideoListItem, Unit> PlayVideoCommand { get; }

        public ReactiveCommand<Unit, Unit> GetNextPageAsyncCommand { get; }

        public ReactiveCommand<Unit, Unit> GoToChatAsyncCommand { get; }

        private async Task SearchAsync()
        {
            IsLoading = true;

            VideoPage = await _youtube.SearchVideosAsync(VideoQuery);

            VideoList = new ObservableCollection<YouTubeVideoListItem>(VideoPage.Results);

            IsLoading = false;
	    }

        private void Play(YouTubeVideoListItem video)
        {
            // TODO: replace mvx interaction
			// _interaction.Raise(Video.Id);
            Console.WriteLine($"Invoke Command Works! {video.Title} {video.Author}");
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
