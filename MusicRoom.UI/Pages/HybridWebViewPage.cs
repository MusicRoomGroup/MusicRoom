using MusicRoom.UI.CustomViews;
using Xamarin.Forms;

namespace MusicRoom.UI.Pages
{
    public class HybridWebViewPage : ContentView
    {
        public HybridWebViewPage()
        {
            BuildPage();
        }

        public HybridWebView BuildWebView()
        {
            var webView = new HybridWebView
            {
                HeightRequest=283,
                WidthRequest=375,
                VerticalOptions=LayoutOptions.FillAndExpand,
                HorizontalOptions=LayoutOptions.FillAndExpand,
                Uri = "Hybrid.html"
            };

            return webView;
        }

        public void BuildPage()
        {
            HybridWebView webView = BuildWebView();

            Content = new StackLayout
            {
                Children =
			    {
					webView
			    }
            };
        }
    }
}
