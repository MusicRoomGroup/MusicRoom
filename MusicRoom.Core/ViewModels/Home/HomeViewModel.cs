using System;
using System.Collections.ObjectModel;
using System.Reactive;
using DynamicData;
using MusicRoom.Core.Models;
using MusicRoom.Core.Services.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace MusicRoom.Core.ViewModels.Home
{
    public class HomeViewModel : ViewModelBase
    {
        #region Reactive Properties
        [Reactive] public string VideoQuery { get; set; }

        [Reactive] private PagedResult<YouTubeVideoListItem> CurrentPage { get; set; }

        [Reactive] public YouTubeVideoListItem Video { get; set; }

        [Reactive] public ObservableCollection<YouTubeVideoListItem> VideoList { get; private set; }

        [Reactive] public int Total { get; private set; }
        #endregion

        #region Observables as Properties
        [ObservableAsProperty] private bool IsLoadingInit { get; }

        [ObservableAsProperty] private bool IsLoadingNext { get; }

        [ObservableAsProperty] public bool IsLoading { get; }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, PagedResult<YouTubeVideoListItem>> SearchAsyncCommand { get; }

        private ReactiveCommand<YouTubeVideoListItem, YouTubeVideoListItem> PlayVideoCommand { get; }

        public ReactiveCommand<Unit, PagedResult<YouTubeVideoListItem>> GetNextPageAsyncCommand { get; }

        public ReactiveCommand<Unit, Unit> GoToChatAsyncCommand { get; }
        #endregion

        public HomeViewModel(IScreen screen = null, IYoutubeSearchService youtube = null) : base(screen)
        {
            UrlPathSegment = "Home";

            youtube ??= Locator.Current.GetService<IYoutubeSearchService>();

            SearchAsyncCommand = ReactiveCommand
                .CreateFromTask(async () => await youtube!.SearchVideosAsync(VideoQuery),
                    this.WhenAnyValue(
                        vm => vm.IsLoading,
                        vm => vm.VideoQuery,
                        (b, q) => !b && !string.IsNullOrEmpty(q)));

            // impossible to have null value for CurrentPage
            GetNextPageAsyncCommand = ReactiveCommand
                .CreateFromTask(async () => await youtube!.GetNextPageAsync(CurrentPage!.Next),
                    this.WhenAnyValue(
                        vm => vm.IsLoading,
                        b => !b));

            PlayVideoCommand = ReactiveCommand.Create<YouTubeVideoListItem, YouTubeVideoListItem>( video => video );

            MessageBus.Current.RegisterMessageSource(PlayVideoCommand);

            // TODO: Replace mvx navigation with reactive ui
            //GoToChatAsyncCommand = ReactiveCommand.CreateFromTask(async () => await _navMan.Navigate<ChatViewModel>());

            SearchAsyncCommand.IsExecuting.ToPropertyEx(this, x => x.IsLoadingInit);

            GetNextPageAsyncCommand.IsExecuting.ToPropertyEx(this, x => x.IsLoadingNext);

            // compose both loading events so easier for UI to know
            this
                .WhenAnyValue(
                     vm => vm.IsLoadingInit,
                     vm => vm.IsLoadingNext,
                     (init, next) => init || next)
                .ToPropertyEx(this, x => x.IsLoading);

            // this commands acts as a state initializer in a way
            SearchAsyncCommand.Subscribe(firstPage =>
            {
                CurrentPage = firstPage;
                Total = CurrentPage.Count;
                VideoList = new ObservableCollection<YouTubeVideoListItem>(CurrentPage.Results);
            });

            GetNextPageAsyncCommand.Subscribe(nextPage =>
            {
                CurrentPage = nextPage;
                Total += CurrentPage.Count;
                VideoList.AddRange(CurrentPage.Results);
            });

            this
                .WhenAnyValue(v => v.Video)
                .WhereNotNull()
                .InvokeCommand(PlayVideoCommand);
        }
    }
}
