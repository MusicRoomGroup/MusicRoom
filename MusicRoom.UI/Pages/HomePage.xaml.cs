using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MusicRoom.Core.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MusicRoom.UI.CustomViews;

namespace MusicRoom.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarTextColor = Color.White;
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            }
        }

        protected override void OnViewModelSet()
        {
			base.OnViewModelSet();
            using var set = CreateBindingSet();
            HybridWebView player = this.FindByName<HybridWebView>("YouTubePlayer");
            set.Bind(player)
                .For(v => v.Interaction)
                .To(vm => vm.Interaction);
        }
    }
}
