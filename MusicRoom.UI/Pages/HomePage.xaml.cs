using System.Reactive.Disposables;
using MvvmCross.Forms.Presenters.Attributes;
using MusicRoom.Core.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace MusicRoom.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class HomePage : ReactiveContentPage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this
                    .Bind(ViewModel, x => x.Uri, x => x.YouTubePlayer.Uri)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.VideoCount, x => x.VideoCount.Text)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.VideoQuery, x => x.TrackQueryEntry.Text)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.GoToChatAsyncCommand, x => x.ChatButton)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.SearchAsyncCommand, x => x.SearchButton)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.IsLoading, x => x.LoadingLabel.IsVisible)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.VideoList, x => x.VideosList.ItemsSource)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.Video, x => x.VideosList.SelectedItem)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.Video, x => x.VideosList.SelectedItem)
                    .DisposeWith(disposable);
                this
                    .Bind(ViewModel, x => x.GetNextPageAsyncCommand, x => x.InfiniteScroll.LoadMoreCommand)
                    .DisposeWith(disposable);
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!(Application.Current.MainPage is NavigationPage navigationPage)) return;
            navigationPage.BarTextColor = Color.White;
            navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
        }
    }
}
