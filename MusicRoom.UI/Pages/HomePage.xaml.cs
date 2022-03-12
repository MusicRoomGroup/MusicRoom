using System.Reactive.Disposables;
using System.Reactive.Linq;
using MusicRoom.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;

namespace MusicRoom.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {
        public HomePage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel,
                        vm => vm.Uri,
                        v => v.YouTubePlayer.Uri)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                        vm => vm.VideoCount,
                        v => v.VideoCount.Text)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                        vm => vm.VideoQuery,
                        v => v.TrackQueryEntry.Text)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                        vm => vm.GoToChatAsyncCommand,
                        v => v.ChatButton)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                        vm => vm.SearchAsyncCommand,
                        v => v.SearchButton)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                        vm => vm.IsLoading,
                        v => v.LoadingLabel.IsVisible)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                        vm => vm.VideoList,
                        v => v.VideosList.ItemsSource)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                        vm => vm.Video,
                        v => v.VideosList.SelectedItem)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                        vm => vm.GetNextPageAsyncCommand,
                        v => v.InfiniteScroll.Command)
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
